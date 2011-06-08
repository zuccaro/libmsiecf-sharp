using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace libmsiecf
{
    /// <summary>
    /// Object-oriented wrapper for libmsiecf
    /// </summary>
    public class IndexDat
    {
        internal IndexDatHandle handle;

        /// <summary>
        /// Constructor - give it an index.dat and it will give you the history!
        /// </summary>
        /// <param name="file"></param>
        public IndexDat(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException();
            }

            if (!file.Exists)
            {
                throw new ArgumentException("file must exist, sheesh");
            }

            int rv = NativeMethods.libmsiecf_file_initialize(out handle, IntPtr.Zero);
            if (rv == 1)
            {
                rv = NativeMethods.libmsiecf_file_open_wide(handle, file.FullName, AccessFlags.Read, IntPtr.Zero);
                if (rv == 1)
                {

                    long len = 0;
                    rv = NativeMethods.libmsiecf_file_get_size(handle, ref len, IntPtr.Zero);
                    if (rv == 1)
                    {
                        this.Length = len;
                    }

                    int items = 0;
                    rv = NativeMethods.libmsiecf_file_get_number_of_items(handle, ref items, IntPtr.Zero);
                    if (rv == 1)
                    {
                        this.ItemCount = items;
                    }

                    rv = NativeMethods.libmsiecf_file_get_number_of_recovered_items(handle, ref items, IntPtr.Zero);
                    if (rv == 1)
                    {
                        this.RecoveredItemCount = items;
                    }
                }
            }
        }

        /// <summary>
        /// Destroys the handle
        /// </summary>
        ~IndexDat()
        {
            this.handle.Close();
        }

        /// <summary>
        /// The length in bytes, declared in the index.dat header
        /// </summary>
        public long Length
        {
            get;
            internal set;
        }

        /// <summary>
        /// The number of items in the allocated portion
        /// </summary>
        public int ItemCount
        {
            get;
            internal set;
        }

        /// <summary>
        /// The number of items found in the unallocated portion
        /// </summary>
        public int RecoveredItemCount
        {
            get;
            internal set;
        }

        /// <summary>
        /// URL items
        /// </summary>
        public IEnumerable<IndexDatURLItem> URLItems
        {
            get
            {
                for (int a = 0; a < ItemCount; a++)
                {
                    IndexDatItemHandle ih;
                    int rv = NativeMethods.libmsiecf_file_get_item(this.handle, a, out ih, IntPtr.Zero);
                    if (rv == 1)
                    {
                        ItemType itemType = ItemType.Unknown;
                        rv = NativeMethods.libmsiecf_item_get_type(ih, ref itemType, IntPtr.Zero);
                        if (rv == 1)
                        {
                            if (itemType == ItemType.URL)
                            {
                                var item = new IndexDatURLItem();

                                #region location
                                int locsize = 0;
                                rv = NativeMethods.libmsiecf_url_get_location_size(ih, ref locsize, IntPtr.Zero);
                                if (rv == 1)
                                {
                                    IntPtr namebuf = Marshal.AllocHGlobal(locsize);
                                    rv = NativeMethods.libmsiecf_url_get_location(ih, namebuf, new UIntPtr((uint)locsize), IntPtr.Zero);

                                    if (rv == 1)
                                    {
                                        byte[] data = new byte[locsize];
                                        Marshal.Copy(namebuf, data, 0, locsize);
                                        item.Location = Encoding.UTF8.GetString(data, 0, locsize - 1);
                                    }
                                }
                                #endregion

                                #region dates

                                long time = 0;
                                rv = NativeMethods.libmsiecf_url_get_primary_time(ih, ref time, IntPtr.Zero);
                                if (rv == 1)
                                {
                                    var dt = DateTime.FromFileTime(time);
                                    item.PrimaryTime = dt;
                                }

                                time = 0;
                                rv = NativeMethods.libmsiecf_url_get_secondary_time(ih, ref time, IntPtr.Zero);
                                if (rv == 1)
                                {
                                    var dt = DateTime.FromFileTime(time);
                                    item.SecondaryTime = dt;
                                }
                                #endregion

                                #region hitcount

                                int hits = 0;
                                rv = NativeMethods.libmsiecf_url_get_number_of_hits(ih, ref hits, IntPtr.Zero);
                                if (rv == 1)
                                {
                                    item.Hits = hits;
                                }
                                #endregion

                                yield return item;
                            }

                            ih.Close();
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }


    /// <summary>
    /// A URL from Index.dat
    /// </summary>
    public class IndexDatURLItem
    {
        /// <summary>
        /// Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Primary time
        /// </summary>
        public DateTime PrimaryTime { get; set; }

        /// <summary>
        /// Secondary time
        /// </summary>
        public DateTime SecondaryTime { get; set; }

        /// <summary>
        /// Expiration time
        /// </summary>
        public DateTime ExpirationTime { get; set; }

        /// <summary>
        /// Last checked time
        /// </summary>
        public DateTime LastCheckedTime { get; set; }

        /// <summary>
        /// Hit count
        /// </summary>
        public int Hits { get; set; }
    }

}
