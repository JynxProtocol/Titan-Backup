// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Titan.API.Models
{
    public partial class MsmDesktopElement
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public long MsmDesktopElementID { get; set; }
        /// <summary>
        /// Source Type
        /// </summary>
        public long SourceTypeID { get; set; }
        /// <summary>
        /// Element Type
        /// </summary>
        public long ElementTypeID { get; set; }
        /// <summary>
        /// Title used on display
        /// </summary>
        public string Title { get; set; }
        public string ToolTip { get; set; }
        /// <summary>
        /// The image to use
        /// </summary>
        public string EmbeddedImage { get; set; }
        public string EmbeddedLargeImage { get; set; }
        /// <summary>
        /// What form should be launched
        /// </summary>
        public string Form { get; set; }
        public string Parameter { get; set; }
        public long FormTypeID { get; set; }
        /// <summary>
        /// The parent item
        /// </summary>
        public long? ParentElement { get; set; }
        /// <summary>
        /// Sequence used for display
        /// </summary>
        public int? Sequence { get; set; }
        public bool DefaultAction { get; set; }
        public bool BeginGroup { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public byte[] OpLock { get; set; }
        /// <summary>
        /// Required By ObjectStore
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public virtual MsmDesktopElementType ElementType { get; set; }
        public virtual MsmDesktopElementFormType FormType { get; set; }
        public virtual MsmDesktopElementSourceType SourceType { get; set; }
    }
}