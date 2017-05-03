using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.Backend.Utils
{
    public class DataTableResponseData<T> where T : class
    {

        /// <summary>
        /// The draw counter that this object is a response to - from the draw parameter sent as part of the data request. 
        /// Note that it is strongly recommended for security reasons that you cast this parameter to an integer, 
        /// rather than simply echoing back to the client what it sent in the draw parameter, in order to prevent Cross Site Scripting (XSS) attacks.
        /// 
        /// Draw counter. This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by 
        /// DataTables (Ajax requests are asynchronous and thus can return out of sequence). This is used as part of the draw return parameter (see below)
        /// </summary>
        public int draw { get; set; }
        
        /// <summary>
        /// Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        public int recordsTotal { get; set; }

        /// <summary>
        /// Total records, after filtering (i.e. the total number of records after filtering has been applied - 
        /// not just the number of records being returned for this page of data).
        /// </summary>
        public int recordsFiltered { get; set; }
        
        /// <summary>
        /// The data to be displayed in the table. This is an array of data source objects, one for each row, which will be used by DataTables. 
        /// Note that this parameter's name can be changed using the ajax option's dataSrc property.
        /// </summary>
        public IEnumerable<T> data { get; set; }

        /// <summary>
        /// Optional: If an error occurs during the running of the server-side processing script, 
        /// you can inform the user of this error by passing back the error message to be displayed using this parameter. 
        /// Do not include if there is no error.
        /// </summary>
        public string error { get; set; }

        /// <summary>
        /// Paging first record indicator. This is the start point in the current data set (0 index based - i.e. 0 is the first record).
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// Number of records that the table can display in the current draw. It is expected that the number of records returned will be equal to this number, 
        /// unless the server has fewer records to return. Note that this can be -1 to indicate that all records 
        /// should be returned (although that negates any benefits of server-side processing!)
        /// </summary>
        public int length { get; set; }
    }

}
