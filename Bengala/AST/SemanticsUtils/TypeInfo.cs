using Bengala.AST.Types;

namespace Bengala.AST.SemanticsUtils
{
    /// <summary>
    /// TypeInfo es usada para almacenar toda la informacion referente a la declaracion de un tipo
    /// </summary>
    public class TypeInfo
    {
        public string TypeId { get; set; }
        public string CodeName { get; set; }
        public TigerType Type { get; set; }
    }
}