using System.ComponentModel;

namespace Core.Catalog.Model.Entity
{
    public class EnumTypes
    {
        public enum TypeStatus
        {
            Disable,
            Active
        }

        public enum TypeError
        {
            Ninguno = 0,
            Ok = 10000,
            ErrorGenerico = 9999,
            NoInformation = 1,
            NoData = 2,
            NoDeterminate = 3,
            DataIncorrect = 4,
            ErrorTimeout = 5,
            InternalError = 6            
        }
        public enum TypeService
        {
            None = 0,
            Frontend = 1,
            BackEnd = 2,
        }
        public enum TypeFileLoad
        {
            Ninguno = 0,
            Project = 1,
            ResultFramework = 2,
            Indicator = 3,
            StrategyFramework = 4,
            Organization = 5,
            Alliance = 6,
            Budget = 7
        }

        public enum TypeStatusProject
        {
            [Description("INACTIVO")]
            Inactivo = 0,
            [Description("ACTIVO")]
            Activo = 1,
            [Description("DISEÑO")]
            Disenio = 2,
            [Description("PUESTA MARCHA")]
            PuestaMarcha = 3,
            [Description("IMPLEMENTACION PLANIFICACION")]
            ImplementacionPlanificacion = 4,
            [Description("FACE CIERRE")]
            FaseCierre = 5,
            [Description("NO APROBADO")]
            NoAprobado = 6,
            [Description("CERRADO")]
            Cerrado = 7,
            [Description("EN CONVERSACIONES")]
            EnConversaciones = 8,
        }

        public enum TypeFieldDiagnostic
        {
            [Description("NONE")]
            none = 0,
            [Description("CHECKBOX")]
            CHECKBOX = 1,
            [Description("TEXT")]
            TEXT = 2,
            [Description("DATE")]
            DATE = 3,

        }

        // Método para buscar un enum a partir de su descripción
        public static bool TryGetEnumFromDescription<TEnum>(string description, out TEnum result) where TEnum : struct
        {
            foreach (var field in typeof(TEnum).GetFields())
            {
                var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                     .Cast<DescriptionAttribute>()
                                     .FirstOrDefault();

                if (attribute?.Description == description)
                {
                    result = (TEnum)field.GetValue(null);
                    return true;
                }
            }

            result = default;
            return false;
        }

    }
}
