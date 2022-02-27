using PM_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entities
{
    [Table("parking_lot")]
    public class ParkingLot : TableEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}