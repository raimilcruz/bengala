#region Usings

using Bengala.AST.SemanticsUtils;
using Bengala.AST.Types;

#endregion

namespace Bengala.Compilation.Prelude
{
    /// <summary>
    /// Cualquier funcion predefinida en el lenguaje puede ser annadida usando esta clase.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FunctionPredifined<T>
    {
        #region Fields and Properties

        public FunctionInfo FunctionInfo { get; protected set; }
        public IGeneratorFunctionCode<T> GeneratorFunctionCode { get; protected set; }
        public ElementGenerator<T> FunctionGenerator { get; private set; }

        #endregion

        #region Constructors

        public FunctionPredifined(FunctionInfo functionInfo, IGeneratorFunctionCode<T> functionGenerator)
            : this(functionInfo, functionGenerator.GenerateCode)
        {
            GeneratorFunctionCode = functionGenerator;
        }

        public FunctionPredifined(FunctionInfo functionInfo, ElementGenerator<T> functionGenerator)
        {
            FunctionInfo = functionInfo;
            FunctionGenerator = functionGenerator;
        }

        #endregion
    }

    /// <summary>
    /// Es usada para annadir una implementacion a una funcion en tiger.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGeneratorFunctionCode<T>
    {
        void GenerateCode(string functionName, T codeInfo);
    }

    /// <summary>
    /// Representa a un metodo que pueda generar codigo para un tipo o funcion predefinida.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">El nombre del elemento que se va a generarar</param>
    /// <param name="codeInfo">Informacion del codigo donde se va a  generar</param>
    public delegate void ElementGenerator<T>(string name, T codeInfo);

    public class PrefinedType<T>
    {
        public PrefinedType(TigerType tigerType, ElementGenerator<T> typeGenerator)
        {
            TypeGenerator = typeGenerator;
            TigerType = tigerType;
        }

        public TigerType TigerType { get; protected set; }
        public ElementGenerator<T> TypeGenerator { get; private set; }
    }
}