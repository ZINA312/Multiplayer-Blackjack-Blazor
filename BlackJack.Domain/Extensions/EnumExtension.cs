using System.Reflection;

namespace BlackJack.Domain.Extensions
{
    public static class EnumExtensions
    {
		public static string GetDisplayName(this Enum en)
        {
            if (en == null)
                return "<none selected>";

            try
            {
                FieldInfo field = en
                    .GetType()
                    .GetField(en.ToString());

                if (field == null)
                    return en.ToString();

                NameAttribute[] attributes = (NameAttribute[])field.GetCustomAttributes(typeof(NameAttribute), false);

                if (attributes.Length > 0)
                    return attributes[0].Name;
                else
                    return en.ToString();
            }
            catch
            {
                return en.ToString();
            }
        }

        public static string GetDisplayDescription(this Enum en)
        {
            if (en == null)
                return "<none selected>";

            try
            {
                FieldInfo field = en.GetType().GetField(en.ToString());

                if (field == null)
                    return en.ToString();

                NameAttribute[] attributes = (NameAttribute[])field.GetCustomAttributes(typeof(NameAttribute), false);

                if (attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return en.ToString();
            }
            catch
            {
                return en.ToString();
            }
        }
    }
}
