using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Helpers
{
	public static class ProductHelper
	{
		public static string GetProductTypeName(ProductTypeEnum type)
		{
			return type switch
			{
				ProductTypeEnum.Frame => "Armazón",
				ProductTypeEnum.Crystal => "Cristal",
				ProductTypeEnum.Accessory => "Accesorio",
				_ => "Desconocido"
			};
		}

		public static string ConvertNumberToGuaraniCurrency(decimal value)
		{
            return value.ToString("C0", new System.Globalization.CultureInfo("es-PY"));
        }
	}
}