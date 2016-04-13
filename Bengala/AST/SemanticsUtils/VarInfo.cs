namespace Bengala.AST.SemanticsUtils
{
    /// <summary>
    /// VarInfo es usada para representar toda la informacion referente a una variable cuando se declara en el Scope.
    /// </summary>
    public class VarInfo
    {
        /// <summary>
        /// Devuelve el nombre con que la varible fue declarada en el codigo.
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// Nombre que se le asgina a la variable cuando se declara en el Scope.Se garantiza que este nombre es unico en el codigo.
        /// </summary>
        public string CodeName { get; set; }

        /// <summary>
        /// Devuelve el tipo de la variable.
        /// </summary>
        public TypeInfo TypeInfo { get; set; }

        /// <summary>
        /// Para saber si la variable oculta a otra que ya existe.
        /// </summary>
        public bool Hides { get; set; }

        /// <summary>
        /// Para saber si la varible es un parametro de funcion.
        /// </summary>
        public bool IsParameterFunction { get; set; }

        /// <summary>
        /// Si IsParameterFunction devuelve true, entonce ParameterNumber devuelve la posicion del parametro. El primer parametro
        /// se considera el 1.
        /// </summary>
        public int ParameterNumber { get; set; }

        /// <summary>
        /// Devuelve o asigna el nombre de la funcion de la cual la variable es parametro.
        /// </summary>
        public string FunctionNameParent { get; set; }

        /// <summary>
        /// Devuelve el nombre de la funcion de la cual esta variable es parametro de funcion
        /// </summary>
        public string ParamInFunction { get; set; }

        /// <summary>
        /// Devuelve true si la variable es una variable local(Se usa en Tiger para el caso del for.
        /// </summary>
        public bool IsLocalVariable { get; set; }

        /// <summary>
        /// Devuelve true si una variable fue declarada dentro de una funcion y es usada por una funcion dentro de esta.
        /// </summary>
        public bool IsUsedForAnotherFunction { get; set; }
    }
}