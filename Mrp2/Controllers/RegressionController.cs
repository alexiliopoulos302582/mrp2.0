using Microsoft.AspNetCore.Mvc;

namespace Mrp2.Controllers
{
    using System;
    using System.Collections.Generic;
    using Mrp2.Models;
    using System.Data;
    using System.IO;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    
    using ExcelDataReader;

    public class RegressionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        


/*
        [HttpPost]
        public IActionResult Calculate()
        {
            // Placeholder action if needed in future; currently unused
            return View("Index");
        }*/




        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;

                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        var table = result.Tables[0];

                        var months = new List<double>();
                        var demands = new List<double>();

                        foreach (DataRow row in table.Rows)
                        {
                            if (double.TryParse(row[0].ToString(), out double month) 
                                && double.TryParse(row[1].ToString(), out double demand))
                            {
                                months.Add(month);
                                demands.Add(demand);
                            }
                        }

                        if (months.Count > 0 && demands.Count > 0)
                        {
                            var regression = new Regression
                            {
                                Months = months.ToArray(),
                                Demands = demands.ToArray()
                            };
                            regression.PerformRegression();

                            // Calculate forecasted values for the next 6 months
                            var forecastedValues = new List<double>();

                            for (int i = 1; i <= 6; i++)
                            {
                                forecastedValues.Add(regression.Forecast(months.Max() + i));
                            }

                            // Pass results to the view
                            ViewBag.Slope = regression.Slope;
                            ViewBag.Intercept = regression.Intercept;
                            ViewBag.ForecastedDemand = regression.ForecastedDemand;
                            ViewBag.R2 = regression.R2;
                            ViewBag.AdjustedR2 = regression.AdjustedR2;
                            ViewBag.StandardError = regression.StandardError;
                            ViewBag.Observations = months.Count;
                            ViewBag.ForecastedValues = forecastedValues;
                        }
                    }
                }
            }

            return View("Index");
        }



    }
}
