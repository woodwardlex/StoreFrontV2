using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontV2.DATA.EF.StoreFrontV2_Metadata
{
    #region Product Metadata
    public class ProductMetadata
    {
        //public int ProductID { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "* Product name is required")]
        [StringLength(50, ErrorMessage = "* Product name must be 50 characters or less")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "* Description is required")]
        [StringLength(100, ErrorMessage = "* Description must be 100 characters or less")]
        public string Description { get; set; }

        [Display(Name = "Category ID")]
        [Required(ErrorMessage = "* Category ID is required")]
        public int CategoryID { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "* Status is required")]
        public int ProdStatusID { get; set; }

        [Display(Name = "Units Sold")]
        [Required(ErrorMessage = "* Units Sold is required")]
        [Range(0, int.MaxValue, ErrorMessage = "* Units Sold must be greater than zero")]
        public int UnitsSold { get; set; }

        [Required(ErrorMessage = "* Price is required")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        [Display(Name = "Shipping ID")]
        [Required(ErrorMessage = "* Shipping ID is required")]
        public int ShippingID { get; set; }

        [Display(Name = "Category ID")]
        [Required(ErrorMessage = "* Category ID is required")]
        public int DeptID { get; set; }

        [Display(Name = "Employee ID")]
        [Required(ErrorMessage = "* Employee ID is required")]
        public int EmployeeID { get; set; }
    }

    [MetadataType(typeof(ProductMetadata))]
    public partial class Product { }
    #endregion

    #region Category Metadata
    public class CategoryMetadata
    {
        //public int CategoryID { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "* Category is required")]
        [StringLength(50, ErrorMessage = "* Category must be 50 characters or less")]
        public string CategoryName { get; set; }
    }

    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category { }
    #endregion

    #region Category Metadata
    public class DepartmentMetadata
    {
        //public int DeptID { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "* Department name is required")]
        [StringLength(50, ErrorMessage = "* Department name must be 50 characters or less")]
        public string DeptName { get; set; }
    }

    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }
    #endregion

    #region Employee Metadata
    public class EmployeeMetadata
    {
        //public int EmployeeID { get; set; }

        [Display(Name = "Direct Report ID")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public Nullable<int> DirectReportID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "* First name is required")]
        [StringLength(10, ErrorMessage = "* First name must be 10 characters or less")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "* Last name is required")]
        [StringLength(50, ErrorMessage = "* Last name must be 50 characters or less")]
        public string LestName { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "* Start Date required")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime StartDate { get; set; }
    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee { }
    #endregion

    #region ProductStatu Metadata
    public class ProductStatuMetadata
    {
        //public int ProdStatusID { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "* Status is required")]
        [StringLength(50, ErrorMessage = "* Status must be 50 characters or less")]
        public string StatusName { get; set; }
    }

    [MetadataType(typeof(ProductStatuMetadata))]
    public partial class ProductStatu { }
    #endregion

    #region Shipper Metadata
    public class ShipperMetadata
    {
        //public int ShippingID { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "* Company is required")]
        public string CompanyName { get; set; }
    }

    [MetadataType(typeof(ShipperMetadata))]
    public partial class Shipper { }
    #endregion
}
