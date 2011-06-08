
namespace libmsiecf
{
    /// <summary>
    /// LIBMSIECF_ACCESS_FLAGS
    /// </summary>
    internal enum AccessFlags
    {
        /// <summary>
        /// LIBMSIECF_ACCESS_FLAG_READ 
        /// </summary>
        Read = 0x01,
        /// <summary>
        /// Reserved: not supported yet
        /// LIBMSIECF_ACCESS_FLAG_WRITE
        /// </summary>
        Write = 0x02,
    };

    /// <summary>
    /// LIBMSIECF_ITEM_TYPES
    /// </summary>
    internal enum ItemType : byte
    {
        /// <summary>
        /// LIBMSIECF_ITEM_TYPE_UNDEFINED
        /// </summary>
        Undefined=0,
        /// <summary>
        /// LIBMSIECF_ITEM_TYPE_URL
        /// </summary>
        URL,
        /// <summary>
        /// LIBMSIECF_ITEM_TYPE_REDIRECTED
        /// </summary>
        Redirect,
        /// <summary>
        /// LIBMSIECF_ITEM_TYPE_LEAK
        /// </summary>
        Leak,
        /// <summary>
        /// LIBMSIECF_ITEM_TYPE_UNKNOWN
        /// </summary>
        Unknown
    };

    /// <summary>
    /// LIBMSIECF_ITEM_FLAGS
    /// </summary>
    internal enum ItemFlags
    {
        /// <summary>
        /// No flags
        /// </summary>
        None = 0,
        /// <summary>
        /// LIBMSIECF_ITEM_FLAG_RECOVERED (1)
        /// </summary>
        Recovered = 0x01,
        /// <summary>
        /// LIBMSIECF_ITEM_FLAG_PARTIAL  (2)
        /// </summary>
        Partial = 0x02,
        /// <summary>
        /// LIBMSIECF_ITEM_FLAG_HASHED (4)
        /// </summary>
        Hashed = 0x04
    };
}
