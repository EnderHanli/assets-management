using Application.Common.Models;

namespace Application
{
    public static class AssetErrors
    {
        #region Product
        public static readonly Error ProductNotFound = new(
            "Product.NotFound", "The product with the speficied id was not found");

        public static readonly Error ProductCurrentlyUsed = new(
            "Product.CurrentlyUsed", "This product can't be deleted as it's currently being used.");
        #endregion

        #region Category
        public static readonly Error CategoryNotFound = new(
            "Category.NotFound", "The category with the speficied id was not found.");

        public static readonly Error CategoryCurrentlyUsed = new(
            "Category.CurrentlyUsed", "This category can't be deleted as it's currently being used.");
        #endregion

        #region Status
        public static readonly Error StatusNotFound = new(
            "Status.NotFound", "The status with the speficied id was not found.");

        public static readonly Error StatusCurrentlyUsed = new(
            "Status.CurrentlyUsed", "This status can't be deleted as it's currently being used.");

        public static readonly Error StatusSystemValue = new(
            "Status.SystemValue", "This is a default status label, and can't be deleted.");
        #endregion

        #region StatusType
        public static readonly Error StatusTypeNotFound = new(
           "StatusType.NotFound", "The status type with the speficied id was not found.");
        #endregion

        #region Assets
        public static readonly Error AssetNotFound = new(
           "Asset.NotFound", "The asset with the speficied id was not found.");
        #endregion

        #region Accessories
        public static readonly Error AccessoryNotFound = new(
           "Accessory.NotFound", "The accessory with the speficied id was not found.");
        #endregion

        #region Consumables
        public static readonly Error ConsumableNotFound = new(
           "Consumable.NotFound", "The consumable with the speficied id was not found.");
        #endregion

        #region Components
        public static readonly Error ComponentNotFound = new(
           "Component.NotFound", "The component with the speficied id was not found.");

        public static readonly Error ComponentCurrentlyUsed = new(
            "Component.CurrentlyUsed", "This component can't be deleted as it's currently being used.");
        #endregion

        #region ControlTimeType
        public static readonly Error ControlTimeTypeNotFound = new(
            "ControlTimeType.NotFound", "The control time type with the speficied id was not found.");
        #endregion

        #region Manufacturer
        public static readonly Error ManufacturerNotFound = new(
            "Manufacturer.NotFound", "The manufacturer with the speficied id was not found.");
        #endregion

        #region Department
        public static readonly Error DepartmentNotFound = new(
            "Department.NotFound", "The department with the speficied id was not found.");
        #endregion
    }
}
