﻿// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.
using System;
using System.Text;

namespace SiliconStudio.Shaders.Ast
{
    /// <summary>
    /// A Storage qualifier.
    /// </summary>
    public class Qualifier : CompositeEnum, IComparable<Qualifier>
    {
        #region Constants and Fields

        /// <summary>
        /// None Enum.
        /// </summary>
        public static readonly Qualifier None = new Qualifier(string.Empty);

        /// <summary>
        /// Order used for sorting.
        /// </summary>
        protected virtual int DefaultOrder => 0;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "Qualifier" /> class.
        /// </summary>
        public Qualifier()
            : base(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Qualifier"/> class.
        /// </summary>
        /// <param name="key">
        /// Name of the enum.
        /// </param>
        protected Qualifier(object key)
            : base(key, true)
        {
        }
        #endregion

        #region Operators

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a post qualifier.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is a post qualifier; otherwise, <c>false</c>.
        /// </value>
        public bool IsPost { get; set; }

        /// <summary>
        ///   Implements the operator ==.
        /// </summary>
        /// <param name = "left">The left.</param>
        /// <param name = "right">The right.</param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static Qualifier operator &(Qualifier left, Qualifier right)
        {
            return OperatorAnd(left, right);
        }

        /// <summary>
        ///   Implements the operator |.
        /// </summary>
        /// <param name = "left">The left.</param>
        /// <param name = "right">The right.</param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static Qualifier operator |(Qualifier left, Qualifier right)
        {
            return OperatorOr(left, right);
        }

        /// <summary>
        ///   Implements the operator ^.
        /// </summary>
        /// <param name = "left">The left.</param>
        /// <param name = "right">The right.</param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static Qualifier operator ^(Qualifier left, Qualifier right)
        {
            return OperatorXor(left, right);
        }

        public string ToString(bool isPost)
        {
            var strBuild = new StringBuilder();
            var str = ToString<Qualifier>(qualifier => qualifier.IsPost == isPost);
            if (!string.IsNullOrEmpty(str))
            {
                if (isPost)
                {
                    strBuild.Append(" ");
                    strBuild.Append(str);
                }
                else
                {
                    strBuild.Append(str);
                    strBuild.Append(" ");
                }
                return strBuild.ToString();
            }

            return string.Empty;
        }
        
        #endregion

        public int CompareTo(Qualifier other)
        {
            if (ReferenceEquals(other, null))
                return 1;

            return DefaultOrder.CompareTo(other.DefaultOrder);
        }
    }
}