using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AxelHarveyStudio.DATA.EF/*.Metadata*/
{



    #region Location
    [MetadataType(typeof(LocationMetadata))]
    public partial class Location { }
    public class LocationMetadata
    {
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Studio Location")]
        public int LocationID { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Location")]
        [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
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
        [UIHint("MultilineText")]
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
      
        [Display(Name = "Asset ID")]
        public int OwnerAssetID { get; set; }

        [Required(ErrorMessage = "*Required")]
        [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
        [Display(Name = "Member Name:")]
        public string AssetName { get; set; }

       
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [StringLength(100, ErrorMessage = "* File Name Must be 100 characters or less.")]
        [Display(Name = "Member Photo:")]
        public string AssetPhoto { get; set; }//nullable

        [StringLength(500, ErrorMessage = "* Must be 500 characters or less.")]
        [Display(Name = "Member Notes")]
        [UIHint("MultilineText")]
        public string OwnerNotes { get; set; }//nullable

        [StringLength(1000, ErrorMessage = "* Must be 1000 characters or less.")]
        [UIHint("MultilineText")]
        [Display(Name = "Notes by Emp")]
        public string EmployeeNotes { get; set; }//nullable

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Active Musician?")]
        public bool IsActive { get; set; }

         
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
       
        public System.DateTime DateAdded { get; set; }

        [StringLength(200, ErrorMessage = "* Must be 200 characters or less.")]
        [Display(Name = "Studio Review")]
        [UIHint("MultilineText")]
        public string Review { get; set; }
    }

    #endregion

    #region Reservation
    [MetadataType(typeof(ReservationMetadata))]
    public partial class Reservation { }
    public class ReservationMetadata
    {
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Reservation")]
        public int ReserationID { get; set; }

        [Required(ErrorMessage = "*Required, Create a Member to book with first! Click 'Members' in the navigation bar.")]
        [Display(Name = "Asset ID")]
        public int OwnerAssetID { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Location:")]
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
    [MetadataType(typeof(PortfolioItemMetadata))]
    public partial class PortfolioItem { }
    public class PortfolioItemMetadata
    {

        
        [Display(Name = "ID")]
        public int JobID { get; set; }

        [Required(ErrorMessage = "*Required")]
        [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
        [Display(Name = "Title")]
        public string JobName { get; set; }

        [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
        public string Photo { get; set; }

        [StringLength(200, ErrorMessage = "* Must be 200 characters or less.")]
        [Display(Name = "Description")]
        [UIHint("MultilineText")]
        public string JobDescription { get; set; }

        [StringLength(200, ErrorMessage = "* Must be 200 characters or less.")]
        [Display(Name = "Client Review")]
        [UIHint("MultilineText")]
        public string JobReview { get; set; }

        [StringLength(200, ErrorMessage = "* Must be 200 characters or less.")]
        public string ProjectLink { get; set; }
    }
    #endregion






}

