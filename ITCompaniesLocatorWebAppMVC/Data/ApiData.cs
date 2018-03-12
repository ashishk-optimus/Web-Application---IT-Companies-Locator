﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace ITCompaniesLocatorWebAppMVC.Data
{
    /// <summary>
    /// It will make Http request to google places api and 
    /// fetch the result through method
    /// </summary>
    public class ApiData
    {
        /// <summary>
        /// Return the response for given city in XmlDocument object for the given baseUrl
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns>XmlDocument</returns>
        public static XmlDocument GiveCompaniesInXml(string baseUrl)
        {
            // Making HTTP Request using baseURL 
            HttpWebRequest req = WebRequest.Create(baseUrl) as HttpWebRequest;

            // Object of XmlDocument to hold the Http Response in XML format
            XmlDocument xmlDoc = new XmlDocument();

            if (req != null)
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    if (resp != null)
                    {
                        // Loading response to XmlDocument object
                        xmlDoc.Load(resp.GetResponseStream() ?? throw new InvalidOperationException());
                    }
                }

            return xmlDoc;
        }
    }
}