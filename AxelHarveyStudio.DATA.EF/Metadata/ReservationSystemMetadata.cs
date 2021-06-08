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
            [Display(Name = "Day Slots")]
            public byte ReservationLimit { get; set; }
        }

        #endregion

        #region OwnerAsset

        [MetadataType(typeof(OwnerAssetMetadata))]
        public partial class OwnerAsset { }
        public class OwnerAssetMetadata
        {
            [Required(ErrorMessage = "*Required")]
            public int OwnerAssetID { get; set; }

            [Required(ErrorMessage = "*Required")]
            [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
            public string AssetName { get; set; }

            [Required(ErrorMessage = "*Required")]          
            public string UserID { get; set; }

            [StringLength(100, ErrorMessage = "* File Name Must be 100 characters or less.")]
            public string AssetPhoto { get; set; }//nullable

            [StringLength(500, ErrorMessage = "* Must be 500 characters or less.")]
            public string OwnerNotes { get; set; }//nullable

            [StringLength(1000, ErrorMessage = "* Must be 1000 characters or less.")]
            public string EmployeeNotes { get; set; }//nullable

            [Required(ErrorMessage = "*Required")]
            public bool IsActive { get; set; }

            [Required(ErrorMessage = "*Required")]
            [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            public System.DateTime DateAdded { get; set; }

            [StringLength(200, ErrorMessage = "* Must be 200 characters or less.")]
            public string Review { get; set; }
        }

        #endregion

        #region Reservation
        [MetadataType(typeof(ReservationMetadata))]
        public partial class Reservation { }
        public class ReservationMetadata
        {
            [Required(ErrorMessage = "*Required")]
            public int ReserationID { get; set; }

            [Required(ErrorMessage = "*Required")]
            public int OwnerAssetID { get; set; }

            [Required(ErrorMessage = "*Required")]
            public int LocationID { get; set; }

            [Required(ErrorMessage = "*Required")]
            [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            public System.DateTime ReservationDate { get; set; }
        }

        #endregion

        #region UserDetail
        [MetadataType(typeof(UserDetailMetadata))]
        public partial class UserDetail { }
        public class UserDetailMetadata
        {

            [Required(ErrorMessage = "*Required")]
            public string UserID { get; set; }

            [Required(ErrorMessage = "*Required")]
            [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "*Required")]
            [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
            public string LastName { get; set; }
        }
        
        #endregion








    }
}
