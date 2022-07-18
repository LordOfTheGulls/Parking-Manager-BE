using Grpc.Core;
using PM_Common.DTO;
using PM_Common.Enums;
using PM_Common.Enums.Parking;
using PM_DAL;
using PM_DAL.Entity;
using PM_DAL.UOW;
using System.Net.WebSockets;

namespace PM_API.Services
{
    public class ParkingService : Parking.ParkingBase
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        private readonly ILogger<ParkingService> _logger;

        public ParkingService(IDbContext<IUnitOfWork> uow, ILogger<ParkingService> logger)
        {
            _uow     = uow    ?? throw new ArgumentNullException(nameof(uow));
            _logger  = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async override Task<LogParkingEventResponse> LogParkingEvent(LogParkingEventRequest request, ServerCallContext context) {

            using var uow = await _uow.OpenAsync();

            await uow.ParkingEventLogRepository.LogEventAsync(new ParkingEventEmitDto()
            {
                EventLotId = request.EventLotId,
                EventType  = (ParkingEventType)request.EventType
            });

            await uow.CommitAsync();

            return new LogParkingEventResponse();
        }

        public async override Task<OpenBarrierResponse> OpenBarrier(OpenBarrierRequest request, ServerCallContext context)
        {
            using var uow = await _uow.OpenAsync();

            bool isAllowedToEnter = await uow.ParkingLotBlacklistRepository.IsLicensePlateBlacklisted(request.LicensePlate);

            await uow.ParkingEventLogRepository.LogEventAsync(new ParkingEventEmitDto()
            {
                EventLotId   = request.LotId,
                EventType    = (isAllowedToEnter ? ParkingEventType.Entrance_Attempt : ParkingEventType.Entrance_Attempt_Blocked)
            });

            await uow.CommitAsync();

            _logger.LogInformation("Event has been logged.");

            return new OpenBarrierResponse(){ 
                IsBlacklisted = isAllowedToEnter 
            };
        }

        public override Task<CloseBarrierResponse> CloseBarrier(CloseBarrierRequest request, ServerCallContext context)
        {
            return null;
        }
    }
} 
