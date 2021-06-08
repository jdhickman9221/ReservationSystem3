using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AxelHarveyStudio.DATA.EF/*.Metadata*/
{
    class ReservationSystemMetadata
    {

        #region Location
        [MetadataType(typeof(LocationMetadata))]
        public partial class Location { }
        public class LocationMetadata
        {
            [Required(ErrorMessage ="*Required")]
            [Display(Name = "Location ID")]
            public int LocationID { get; set; }

            [Required(ErrorMessage = "*Required")]
            [Display(Name = "Location")]
            [StringLength(50, ErrorMessage ="* Must be 50 characters or less.")]
            public string LocationName { get; set; }

            [Required(ErrorMessage = "*Required")]
            [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
            public string Address { get; set; }

            [Required(ErrorMessage = "*Required")]
            [StringLength(100, ErrorMessage = "* Must be 100 characters or less.")]
            public string City { get; set; }

            [Required(ErrorMessage = "*Required")]
            [StringLength(2, ErrorMessage = "* Must be 2 characters.")]
            public string State { get; set; }

            [Required(ErrorMessage = "*Required")]
            [StringLength(5, ErrorMessage = "* Must be 5 characters.")]
            [Display(Name = "Zip")]
            public string ZipCode { get; set; }

            [Required(ErrorMessage = "*Required")]
            [Display(Name = "Reservation Limit")]
            public byte ReservationLimit { get; set; }
        }

        #endregion

        #region OwnerAsset

        [MetadataType(typeof(OwnerAssetMetadata))]
        public partial class OwnerAsset { }
        public class OwnerAssetMetadata
        {
            public int OwnerAssetID { get; set; }
            public string AssetName { get; set; }
            public string UserID { get; set; }
            public string AssetPhoto { get; set; }
            public string OwnerNotes { get; set; }
            public string EmployeeNotes { get; set; }
            public bool IsActive { get; set; }
            public System.DateTime DateAdded { get; set; }
            public string Review { get; set; }
        }

        #endregion

        #region Reservation
        [MetadataType(typeof(ReservationMetadata))]
        public partial class Reservation { }
        public class ReservationMetadata
        {
            public int ReserationID { get; set; }
            //public int OwnerAssetID { get; set; }
            //public int LocationID { get; set; }
            public System.DateTime ReservationDate { get; set; }
        }

        #endregion

        #region UserDetail
        [MetadataType(typeof(UserDetailMetadata))]
        public partial class UserDetail { }
        public class UserDetailMetadata
        {
            //public string UserID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        
        #endregion








    }
}
