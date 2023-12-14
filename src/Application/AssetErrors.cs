using Application.Common.Models;

namespace Application
{
    public static class AssetErrors
    {
        public static readonly Error ProductNotFound = new(
            "Product.NotFound", "The product with the speficied id was not found");

        public static readonly Error CategoryNotFound = new(
            "Category.NotFound", "The category with the speficied id was not found.");

        public static readonly Error ManufacturerNotFound = new(
            "Manufacturer.NotFound", "The manufacturer with the speficied id was not found.");
    }
}
