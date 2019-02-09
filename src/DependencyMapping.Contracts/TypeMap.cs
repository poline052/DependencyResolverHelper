using System;


namespace Com.DependencyMappingContracts
{
   public class TypeMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMap"/> class.
        /// </summary>
        /// <param name="typeFrom">
        /// The type from.
        /// </param>
        /// <param name="typeTo">
        /// The type to.
        /// </param>
        public TypeMap(Type typeFrom, Type typeTo)
        {
            this.TypeFrom = typeFrom;
            this.TypeTo = typeTo;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMap"/> class.
        /// </summary>
        /// <param name="typeFrom">
        /// The type from.
        /// </param>
        /// <param name="typeTo">
        /// The type to.
        /// <param name="lifestyle">
        /// life style.
        /// </param>
        public TypeMap(Type typeFrom, Type typeTo, ObjectLifeStyle lifestyle)
        {
            this.TypeFrom = typeFrom;
            this.TypeTo = typeTo;
            this.Lifestyle = lifestyle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMap"/> class.
        /// </summary>
        /// <param name="typeFrom">
        /// The type from.
        /// </param>
        /// <param name="typeTo">
        /// The type to.
        /// <param name="priority">
        /// priority.
        /// </param>
        public TypeMap(Type typeFrom, Type typeTo, int priority)
        {
            this.TypeFrom = typeFrom;
            this.TypeTo = typeTo;
            this.Priority = priority;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMap"/> class.
        /// </summary>
        /// <param name="typeFrom">
        /// The type from.
        /// </param>
        /// <param name="typeTo">
        /// The type to.
        /// <param name="lifestyle">
        /// life style.
        /// </param>
        /// <param name="priority">
        /// priority.
        /// </param>
        public TypeMap(Type typeFrom, Type typeTo, ObjectLifeStyle lifestyle,int priority)
        {
            this.TypeFrom = typeFrom;
            this.TypeTo = typeTo;
            this.Lifestyle = lifestyle;
            this.Priority = priority;
        }

        public ObjectLifeStyle? Lifestyle { get; private set; }

        /// <summary>
        ///     Gets the type from.
        /// </summary>
        /// <value>
        ///     The type from.
        /// </value>
        public Type TypeFrom { get; private set; }

        /// <summary>
        ///     Gets the type to.
        /// </summary>
        /// <value>
        ///     The type to.
        /// </value>
        public Type TypeTo { get; private set; }
        public int Priority { get; set; }
    }
}
