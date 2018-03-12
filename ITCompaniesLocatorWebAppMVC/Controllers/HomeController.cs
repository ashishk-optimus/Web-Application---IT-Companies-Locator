﻿using ITCompaniesLocatorWebAppMVC.Models;
using ITCompaniesLocatorWebAppMVC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITCompanyLocatorWebAppMVC.Controllers
{
    /// <summary>
    /// Home Controller which take Http requests and 
    /// route it to the particular function particular action
    /// </summary>
    public class HomeController : Controller
    {
        private static string _cityName;
        private CompanyDetails[] _company;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchActionResult()
        {
            if (Request.Form.AllKeys.Contains("cityName"))
            {
                // Release all the acquired resources(if any) for new Http Search request
                if (SearchCompany.CountPageList != 0)
                {
                    SearchCompany.ReleaseResource();
                }

                _cityName = Request.Form["cityName"].ToString();

                // Getting list of objects for all the companies
                _company = SearchCompany.GiveCompanyDetails(_cityName);
            }
            else
            {
                if (Request.Form.AllKeys.Contains("nextButton"))
                {
                    if (SearchCompany.CountPrevClicked != 0)
                    {
                        // Getting List of objects for all the next available companies
                        _company = SearchCompany.GetNextResults();
                    }
                    else
                    {
                        // Getting list of objects for all the companies for available token
                        _company = SearchCompany.GiveCompanyDetails(_cityName, SearchCompany.NextPageToken);
                    }
                }

                if (Request.Form.AllKeys.Contains("prevButton"))
                {
                    // Getting List of objects for all the previous available companies
                    _company = SearchCompany.GetPreviousResults();
                }
            }

            ViewBag.Companies = _company;
            ViewData["CityName"] = _cityName;
            ViewData["isToken"] = SearchCompany.IsToken;
            ViewData["hasPreviousPage"] = SearchCompany.HasPreviousPage;
            ViewBag.AvailableResults = SearchCompany.CountPageList;

            return View();
        }
    }
}
