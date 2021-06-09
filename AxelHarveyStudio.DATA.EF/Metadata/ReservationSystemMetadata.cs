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

            [StringLength(200, ErrorMessage = "* Must be 200 characters.")]
            public string Description { get; set; }

            [Display(Name = "Logo")]
            [StringLength(100, ErrorMessage = "* Must be 100 characters.")]
            public string LocationLogo { get; set; }
        }

        #endregion

        #region OwnerAsset

        [MetadataType(typeof(OwnerAssetMetadata))]
        public partial class OwnerAsset { }
        public class OwnerAssetMetadata
        {
            [Required(ErrorMessage = "*Required")]
            [Display(Name = "Asset ID")]
            public int OwnerAssetID { get; set; }

            [Required(ErrorMessage = "*Required")]
            [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
            [Display(Name = "Name")]
            public string AssetName { get; set; }

            [Required(ErrorMessage = "*Required")]
            [Display(Name = "User ID")]
            public string UserID { get; set; }

            [StringLength(100, ErrorMessage = "* File Name Must be 100 characters or less.")]
            [Display(Name = "Asset Photo")]
            public string AssetPhoto { get; set; }//nullable

            [StringLength(500, ErrorMessage = "* Must be 500 characters or less.")]
            [Display(Name = "Notes by Client")]
            public string OwnerNotes { get; set; }//nullable

            [StringLength(1000, ErrorMessage = "* Must be 1000 characters or less.")]
            [Display(Name = "Notes by Emp")]
            public string EmployeeNotes { get; set; }//nullable

            [Required(ErrorMessage = "*Required")]
            [Display(Name = "Active?")]
            public bool IsActive { get; set; }

            [Required(ErrorMessage = "*Required")]
            [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            [Display(Name = "Added On:")]
            public System.DateTime DateAdded { get; set; }

            [StringLength(200, ErrorMessage = "* Must be 200 characters or less.")]
            [Display(Name = "Studio Review")]
            public string Review { get; set; }
        }

        #endregion

        #region Reservation
        [MetadataType(typeof(ReservationMetadata))]
        public partial class Reservation { }
        public class ReservationMetadata
        {
            [Required(ErrorMessage = "*Required")]
            [Display(Name = "Reseration")]
            public int ReserationID { get; set; }

            [Required(ErrorMessage = "*Required")]
            [Display(Name = "Asset ID")]
            public int OwnerAssetID { get; set; }

            [Required(ErrorMessage = "*Required")]
            [Display(Name = "Location")]
            public int LocationID { get; set; }

            [Required(ErrorMessage = "*Required")]
            [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            [Display(Name = "Reservation On:")]
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
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "*Required")]
            [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
        }

        #endregion

        #region PorfolioItems
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "ID")]
        public int JobID { get; set; }

        [Required(ErrorMessage = "*Required")]
        [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
        [Display(Name = "Title")]
        public string JobName { get; set; }

        [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
        public string Photo { get; set; }

        [StringLength(200, ErrorMessage = "* Must be 200 characters or less.")]
        [Display(Name ="Description")]
        public string JobDescription { get; set; }

        [StringLength(200, ErrorMessage = "* Must be 200 characters or less.")]
        [Display(Name = "Client Review")]
        public string JobReview { get; set; }

        #endregion






    }
}
