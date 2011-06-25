using System;
using System.Runtime.InteropServices;

namespace libmsiecf
{
    /// <summary>
    /// Safehandle to deal with libmsiecf file handle neatly
    /// </summary>
    internal class IndexDatHandle : SafeHandle
    {
        /// <summary>
        /// ctor
        /// </summary>
        public IndexDatHandle()
            : base(IntPtr.Zero, true)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            bool ok = false;
            if (this.handle != IntPtr.Zero)
            {
                int rv = NativeMethods.libmsiecf_file_close(this.handle, IntPtr.Zero);
                ok = rv == 0;
            }
            return ok;
        }
    }

    /// <summary>
    /// Safehandle to deal with libmsiecf items neatly
    /// </summary>
    internal class IndexDatItemHandle : SafeHandle
    {
        public IndexDatItemHandle()
            : base(IntPtr.Zero, true)
        {
        }

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            bool ok = false;
            if (this.handle != IntPtr.Zero)
            {
                int rv = NativeMethods.libmsiecf_item_free(ref this.handle, IntPtr.Zero);
                ok = rv == 0;
            }
            return ok;
        }
    }

    internal class NativeMethods
    {
        const string library = "libmsiecf"; //.so, .dylib, .dll, etc

        /// <summary>
        /// Initializes the file.  Make sure the value file is pointing to is set to NULL. Returns 1 if successful or -1 on error
        /// LIBMSIECF_EXTERN int libmsiecf_file_initialize(libmsiecf_file_t **file,libmsiecf_error_t **error );
        /// </summary>
        /// <param name="fileHandle"></param>
        /// <param name="filename"></param>
        /// <param name="flags"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [DllImport(library, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_file_initialize(out IndexDatHandle handle, IntPtr error);

        //LIBMSIECF_EXTERN int libmsiecf_file_open_wide(libmsiecf_file_t *file, const wchar_t *filename, int flags, liberror_error_t **error );
        [DllImport(library, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_file_open_wide(IndexDatHandle fileHandle, string filename, AccessFlags flags, IntPtr error);

        /// <summary>
        /// Closes a file. Returns 0 if successful or -1 on error
        /// </summary>
        /// <param name="fileHandle">libmsiecf_file_t* </param>
        /// <param name="error">libmsiecf_error_t** </param>
        /// <returns>Returns 0 if successful or -1 on error</returns>
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_file_close(IntPtr fileHandle, IntPtr error);


        //LIBMSIECF_EXTERN int libmsiecf_file_get_size( libmsiecf_file_t *file, size64_t *size, libmsiecf_error_t **error );
        /// <summary>
        /// Retrieves the file size* Returns 1 if successful or -1 on error
        /// </summary>
        /// <param name="fileHandle"></param>
        /// <param name="filesize"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_file_get_size(IndexDatHandle fileHandle, ref long filesize, IntPtr error);

        //int libmsiecf_file_get_number_of_items( libmsiecf_file_t *file, int *number_of_items, libmsiecf_error_t **error );
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_file_get_number_of_items(IndexDatHandle fileHandle, ref int numberOfItems, IntPtr error);

        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_file_get_number_of_recovered_items(IndexDatHandle fileHandle, ref int numberOfRecoveredItems, IntPtr error);

        //LIBMSIECF_EXTERN int libmsiecf_file_get_number_of_cache_directories(libmsiecf_file_t *file,int *number_of_cache_directories,liberror_error_t **error );
        /// <summary>
        /// Retrieves the number of cache directories
        /// </summary>
        /// <param name="fileHandle"></param>
        /// <param name="numberOfCacheDirectories"></param>
        /// <param name="error"></param>
        /// <returns>Returns 1 if successful or -1 on error</returns>
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_file_get_number_of_cache_directories(IndexDatHandle fileHandle, ref int numberOfCacheDirectories, IntPtr error);

        
        //LIBMSIECF_EXTERN int libmsiecf_file_get_cache_directory_name(libmsiecf_file_t *file,int cache_directory_index,uint8_t *cache_directory_name,size_t cache_directory_name_size,liberror_error_t **error );
        /// <summary>
        /// Retrieves the UTF-8 encoded name of a certain cache directory.  
        /// The size should include the end of string character.  
        /// The cache directory name consists of 8 characters + end of string character
        /// </summary>
        /// <param name="fileHandle"></param>
        /// <param name="cacheDirectoryIndex"></param>
        /// <param name="ptrToInt8LocationString"></param>
        /// <param name="location_size"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_file_get_cache_directory_name(IndexDatHandle fileHandle, int cacheDirectoryIndex, IntPtr ptrToInt8LocationString, UIntPtr location_size, IntPtr error);

        /// <summary>
        /// Retrieves the UTF-8 encoded name of a certain cache directory.  
        /// The size should include the end of string character.  
        /// The cache directory name consists of 8 characters + end of string character
        /// </summary>
        /// <param name="fileHandle"></param>
        /// <param name="cacheDirectoryIndex"></param>
        /// <param name="location"></param>
        /// <param name="locSize"></param>
        /// <param name="error"></param>
        /// <returns>Returns 1 if successful or -1 on error</returns>
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_file_get_cache_directory_name(IndexDatHandle fileHandle, int cacheDirectoryIndex, System.Text.StringBuilder location, uint locSize, IntPtr error);

        
        /// <summary>
        /// Retrieves the item for the specific index
        /// Returns 1 if successful or -1 on error
        /// </summary>
        /// <remarks>
        /// LIBMSIECF_EXTERN int libmsiecf_file_get_item(libmsiecf_file_t *file, int item_index, libmsiecf_item_t **item, libmsiecf_error_t **error );
        /// </remarks>
        /// <param name="fileHandle"></param>
        /// <param name="itemIndex"></param>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_file_get_item(IndexDatHandle fileHandle, int itemIndex, out IndexDatItemHandle item, IntPtr error);

        //LIBMSIECF_EXTERN int libmsiecf_file_get_recovered_item( libmsiecf_file_t *file, int recovered_item_index, libmsiecf_item_t **recovered_item, libmsiecf_error_t **error );
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_file_get_recovered_item(IndexDatHandle fileHandle, int itemIndex, out IndexDatItemHandle item, IntPtr error);

        #region items

        /// <summary>
        ///  Retrieves the type
        /// Determines the item type if neccessary
        /// Returns 1 if successful or -1 on error
        /// </summary>
        /// <remarks>int libmsiecf_item_get_type(libmsiecf_item_t *item, int8_t *item_type, liberror_error_t **error )</remarks>
        /// <param name="fileHandle"></param>
        /// <param name="itemType"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_item_get_type(IndexDatItemHandle fileHandle, ref ItemType itemType, IntPtr error);

        //LIBMSIECF_EXTERN int libmsiecf_url_get_primary_time( libmsiecf_item_t *url, uint64_t *primary_time, libmsiecf_error_t **error );
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_primary_time(IndexDatItemHandle itemHandle, ref long time64, IntPtr error);

        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_secondary_time(IndexDatItemHandle itemHandle, ref long time64, IntPtr error);

        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_expiration_time(IndexDatItemHandle itemHandle, ref uint time32, IntPtr error);

        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_last_checked_time(IndexDatItemHandle itemHandle, ref uint time32, IntPtr error);

        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_cached_file_size(IndexDatItemHandle itemHandle, ref long size, IntPtr error);

        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_cache_directory_index(IndexDatItemHandle itemHandle, ref byte index, IntPtr error);

        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_number_of_hits(IndexDatItemHandle itemHandle, ref int numHits, IntPtr error);

        //LIBMSIECF_EXTERN int libmsiecf_url_get_location_size( libmsiecf_item_t *url, size_t *location_size, libmsiecf_error_t **error );
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_location_size(IndexDatItemHandle itemHandle, ref int location_size, IntPtr error);

        //LIBMSIECF_EXTERN int libmsiecf_url_get_location( libmsiecf_item_t *url, uint8_t *location, size_t location_size, libmsiecf_error_t **error );
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_location(IndexDatItemHandle itemHandle, IntPtr ptrToInt8LocationString, UIntPtr location_size, IntPtr error);

        //LIBMSIECF_EXTERN int libmsiecf_url_get_filename_size( libmsiecf_item_t *url, size_t *filename_size, libmsiecf_error_t **error );
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_filename_size(IndexDatItemHandle itemHandle, IntPtr ptrToSizeT, IntPtr error);

        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_filename_size(IndexDatItemHandle itemHandle, ref int size, IntPtr error);

        //LIBMSIECF_EXTERN int libmsiecf_url_get_filename( libmsiecf_item_t *url, uint8_t *filename, size_t filename_size, libmsiecf_error_t **error );
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_filename(IndexDatItemHandle itemHandle, IntPtr ptrToInt8LocationString, int location_size, IntPtr error);

        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_filename(IndexDatItemHandle itemHandle, byte[] ptrToInt8LocationString, int location_size, IntPtr error);

        //[DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int libmsiecf_url_get_filename(IndexDatItemHandle itemHandle, System.Text.StringBuilder filenameBuffer, uint location_size, IntPtr error);

        //LIBMSIECF_EXTERN int libmsiecf_url_get_data_size( libmsiecf_item_t *url, size_t *data_size, libmsiecf_error_t **error );
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_data_size(IndexDatItemHandle itemHandle, IntPtr ptrToSizeT, IntPtr error);

        //LIBMSIECF_EXTERN int libmsiecf_url_get_data( libmsiecf_item_t *url, uint8_t *data, size_t data_size, libmsiecf_error_t **error );
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_data(IndexDatItemHandle itemHandle, IntPtr ptrToInt8LocationString, UIntPtr location_size, IntPtr error);

        //LIBMSIECF_EXTERN int libmsiecf_url_get_data( libmsiecf_item_t *url, uint8_t *data, size_t data_size, libmsiecf_error_t **error );
        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_url_get_data(IndexDatItemHandle itemHandle, System.Text.StringBuilder data, uint location_size, IntPtr error);

        [DllImport(library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int libmsiecf_item_free(ref IntPtr itemHandle, IntPtr error);

        #endregion
    }
}
