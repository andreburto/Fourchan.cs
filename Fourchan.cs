/*****
 * Fourchan.cs
 * By: Andrew Burton
 * Date: 09/10/2014
 * To Do: Everything else besides parsing the boards list JSON
 *****/

using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Fourchan
{
    public class Fourchan
    {
        /*****
         * URL strings
         *****/
        public string boards_url = "http://a.4cdn.org/boards.json";
        public string catalog_url = "http://a.4cdn.org/{0}/catalog.json";
        public string thread_url = "http://a.4cdn.org/{0}/thread/{1}.json";
        public string page_url = "http://a.4cdn.org/{0}/{1}.json";
        public string threads_url = "http://a.4cdn.org/{0}/threads.json";

        /*****
         * JSON Subclasses
         *****/
        [DataContract]
        public class Boards
        {
            [DataMember(Name = "boards")]
            public Board[] boards { get; set; }
        }

        [DataContract]
        public class Board
        {
            [DataMember(Name = "board")]
            public string board { get; set; }
            [DataMember(Name = "title")]
            public string title { get; set; }
            [DataMember(Name = "ws_board")]
            public int ws_board { get; set; }
            [DataMember(Name = "per_page")]
            public int per_page { get; set; }
            [DataMember(Name = "pages")]
            public int pages { get; set; }
            [DataMember(Name = "max_filesize")]
            public Int64 max_filesize { get; set; }
            [DataMember(Name = "max_webm_filesize")]
            public Int64 max_webm_filesize { get; set; }
            [DataMember(Name = "max_comment_chars")]
            public int max_comment_chars { get; set; }
            [DataMember(Name = "bump_limit")]
            public int bump_limit { get; set; }
            [DataMember(Name = "image_limit")]
            public int image_limit { get; set; }
            [DataMember(Name = "cooldowns")]
            public Cooldowns cooldowns { get; set; }
        }

        [DataContract]
        public class Cooldowns
        {
            [DataMember(Name = "threads")]
            public int threads { get; set; }
            [DataMember(Name = "replies")]
            public int replies { get; set; }
            [DataMember(Name = "images")]
            public int images { get; set; }
            [DataMember(Name = "replies_intra")]
            public int replies_intra { get; set; }
            [DataMember(Name = "images_intra")]
            public int images_intra { get; set; }
        }

        /*****
         * Create the URL's to GET
         *****/
        public string GetCatalogUrl(string board) {
            return String.Format(this.catalog_url, board);
        }

        /*****
         * Parse the JSON into specific objects
         *****/
        public Boards ParseBoards(string address)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Boards));
            object objResponse = jsonSerializer.ReadObject(this.GetJson(address));
            Boards jsonResponse = objResponse as Boards;
            return jsonResponse;
        }

        /*****
         * Fetch the JSON
         *****/
        public Stream GetJson(string address) {
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(String.Format("HTTP ERROR {0}: {1}",
                                    response.StatusCode, response.StatusDescription));
            }

            return response.GetResponseStream();
        }
    }
}
