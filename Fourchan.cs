/**********
 * Fourchan.cs
 * By: Andrew Burton
 * -----
 * Date: 09/10/2014
 * To Do: Everything else besides parsing the boards list JSON
 * *****
 * Date: 09/21/2014
 * Working on: setting up classes for all JSON from 4chan
 **********/

using System;
using System.Collections;
using System.Collections.Generic;
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
        public string post_url = "http://a.4cdn.org/{0}/thread/{1}.json";

        /*****
         * JSON Subclasses
         *****/
        [DataContract]
        public class Board
        {
            [DataMember(Name = "board")]
            public string board { get; set; }
            [DataMember(Name = "title")]
            public string title { get; set; }
            [DataMember(Name = "ws_board")]
            public Int64 ws_board { get; set; }
            [DataMember(Name = "per_page")]
            public Int64 per_page { get; set; }
            [DataMember(Name = "pages")]
            public Int64 pages { get; set; }
            [DataMember(Name = "max_filesize")]
            public Int64 max_filesize { get; set; }
            [DataMember(Name = "max_webm_filesize")]
            public Int64 max_webm_filesize { get; set; }
            [DataMember(Name = "max_comment_chars")]
            public Int64 max_comment_chars { get; set; }
            [DataMember(Name = "bump_limit")]
            public Int64 bump_limit { get; set; }
            [DataMember(Name = "image_limit")]
            public Int64 image_limit { get; set; }
            [DataMember(Name = "cooldowns")]
            public Cooldowns cooldowns { get; set; }
        }

        [DataContract]
        public class Boards
        {
            [DataMember(Name = "boards")]
            public Board[] boards { get; set; }
        }

        [DataContract]
        public class Catalog
        {
            [DataMember(Name = "page")]
            public Int64 page { get; set; }
            [DataMember(Name = "threads")]
            public Thread[] threads { get; set; }
        }

        [DataContract]
        public class Cooldowns
        {
            [DataMember(Name = "threads")]
            public Int64 threads { get; set; }
            [DataMember(Name = "replies")]
            public Int64 replies { get; set; }
            [DataMember(Name = "images")]
            public Int64 images { get; set; }
            [DataMember(Name = "replies_intra")]
            public Int64 replies_intra { get; set; }
            [DataMember(Name = "images_intra")]
            public Int64 images_intra { get; set; }
        }

        [DataContract]
        public class Page
        {
            [DataMember(Name = "page")]
            public Int64 page { get; set; }
            [DataMember(Name = "threads")]
            public Thread[] threads { get; set; }
        }

        [DataContract]
        public class Post
        {
            [DataMember(Name = "no")]
            public Int64 no { get; set; }
            [DataMember(Name = "resto")]
            public Int64 resto { get; set; }
            [DataMember(Name = "sticky")]
            public Int64 sticky { get; set; }
            [DataMember(Name = "closed")]
            public Int64 closed { get; set; }
            [DataMember(Name = "archived")]
            public Int64 archived { get; set; }
            [DataMember(Name = "now")]
            public string now { get; set; }
            [DataMember(Name = "time")]
            public Int64 time { get; set; }
            [DataMember(Name = "name")]
            public string name { get; set; }
            [DataMember(Name = "trip")]
            public string trip { get; set; }
            [DataMember(Name = "id")]
            public string id { get; set; }
            [DataMember(Name = "capcode")]
            public string capcode { get; set; }
            [DataMember(Name = "country")]
            public string country { get; set; }
            [DataMember(Name = "country_name")]
            public string country_name { get; set; }
            [DataMember(Name = "sub")]
            public string sub { get; set; }
            [DataMember(Name = "com")]
            public string com { get; set; }
            [DataMember(Name = "tim")]
            public Int64 tim { get; set; }
            [DataMember(Name = "filename")]
            public string filename { get; set; }
            [DataMember(Name = "ext")]
            public string ext { get; set; }
            [DataMember(Name = "fsize")]
            public Int64 fsize { get; set; }
            [DataMember(Name = "md5")]
            public string md5 { get; set; }
            [DataMember(Name = "w")]
            public Int64 w { get; set; }
            [DataMember(Name = "h")]
            public Int64 h { get; set; }
            [DataMember(Name = "tn_w")]
            public Int64 tn_w { get; set; }
            [DataMember(Name = "tn_h")]
            public Int64 tn_h { get; set; }
            [DataMember(Name = "filedeleted")]
            public Int64 filedeleted { get; set; }
            [DataMember(Name = "spoiler")]
            public Int64 spoiler { get; set; }
            [DataMember(Name = "custom_spoiler")]
            public Int64 custom_spoiler { get; set; }
            [DataMember(Name = "omitted_posts")]
            public Int64 omitted_posts { get; set; }
            [DataMember(Name = "omitted_images")]
            public Int64 omitted_images { get; set; }
            [DataMember(Name = "replies")]
            public Int64 replies { get; set; }
            [DataMember(Name = "images")]
            public Int64 images { get; set; }
            [DataMember(Name = "bumplimit")]
            public Int64 bumplimit { get; set; }
            [DataMember(Name = "imagelimit")]
            public Int64 imagelimit { get; set; }
            [DataMember(Name = "capcode_replies")]
            public Hashtable capcode_replies { get; set; }
            [DataMember(Name = "last_modified")]
            public Int64 last_modified { get; set; }
            [DataMember(Name = "tag")]
            public string tag { get; set; }
            [DataMember(Name = "semantic_url")]
            public string semantic_url { get; set; }
        }

        [DataContract]
        public class Posts
        {
            [DataMember(Name = "posts")]
            public Post[] posts { get; set; }
        }

        [DataContract]
        public class Thread
        {
            [DataMember(Name = "no")]
            public string no { get; set; }
            [DataMember(Name = "now")]
            public string now { get; set; }
            [DataMember(Name = "name")]
            public string name { get; set; }
            [DataMember(Name = "com")]
            public string com { get; set; }
            [DataMember(Name = "filename")]
            public string filename { get; set; }
            [DataMember(Name = "ext")]
            public string ext { get; set; }
            [DataMember(Name = "w")]
            public Int64 w { get; set; }
            [DataMember(Name = "h")]
            public Int64 h { get; set; }
            [DataMember(Name = "tn_w")]
            public Int64 tn_w { get; set; }
            [DataMember(Name = "tn_h")]
            public Int64 tn_h { get; set; }
            [DataMember(Name = "tim")]
            public Int64 tim { get; set; }
            [DataMember(Name = "time")]
            public Int64 time { get; set; }
            [DataMember(Name = "md5")]
            public string md5 { get; set; }
            [DataMember(Name = "fsize")]
            public Int64 fsize { get; set; }
            [DataMember(Name = "restno")]
            public Int64 resto { get; set; }
            [DataMember(Name = "bumplimit")]
            public Int64 bumplimit { get; set; }
            [DataMember(Name = "imagelimit")]
            public Int64 imagelimit { get; set; }
            [DataMember(Name = "semantic_url")]
            public string semantic_url { get; set; }
            [DataMember(Name = "replies")]
            public Int64 replies { get; set; }
            [DataMember(Name = "images")]
            public Int64 images { get; set; }
            [DataMember(Name = "omitted_posts")]
            public Int64 omitted_posts { get; set; }
            [DataMember(Name = "omitted_images")]
            public Int64 omitted_images { get; set; }
            [DataMember(Name = "last_replies")]
            public Post[] last_replies { get; set; }
        }

        [DataContract]
        public class Threads
        {
            [DataMember(Name = "threads")]
            public Thread[] threads { get; set; }
        }

        /*****
         * Create the URL's to GET
         *****/
        public string GetCatalogUrl(string board)
        {
            return String.Format(this.catalog_url, board);
        }

        public string GetPageUrl(string board, string number)
        {
            return String.Format(this.page_url, board, number);
        }

        public string GetPostUrl(string board, string number)
        {
            return String.Format(this.post_url, board, number);
        }

        public string GetTheadUrl(string board, string number)
        {
            return String.Format(this.thread_url, board, number);
        }

        public string GetTheadsUrl(string board)
        {
            return String.Format(this.threads_url, board);
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

        public Catalog ParseCatalog(string address)
        {
        }

        public Thread ParseThread(string address)
        {

        }

        public List<Page> GetPages(string address)
        {

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
