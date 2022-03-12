﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Entity
{
    [Table("parking_lot")]
    public class ParkingLot
    {
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("lot_name")]
        public string Name { get; set; }

        [Column("business_name")]
        public string BusinessName { get; set; } = "";

        [Column("lot_description")]
        public string? Description { get; set; }

        [Column("lot_latitude")]
        public decimal Latitude { get; set; }

        [Column("lot_longitude")]
        public decimal Longitude { get; set; }

        [Column("mobile_access")]
        public bool HasMobileAccess { get; set; } = false;

        [Column("lot_covered")]
        public bool IsLotCovered { get; set; } = false;

        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [Column("date_modified")]
        public DateTime? DateModified { get; set; }

        [Column("active")]
        public bool IsActive { get; set; } = true;


        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }

    }
}
