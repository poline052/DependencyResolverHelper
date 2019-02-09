using Microsoft.AspNetCore.Mvc;
using Com.SampleLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.WebService.Controllers
{
    //[Route("api/[controller]")]
    public class MathController: Controller
    {
        public IFibonacciGenerator FibonacciGenerator { get; set; }
        public MathController(IFibonacciGenerator fibonacciGenerator)
        {
            this.FibonacciGenerator = fibonacciGenerator;
        }

        [HttpGet]
        public long GenerateFibonacci(int n)
        {
            return this.FibonacciGenerator.Generate(n);
        }
    }
}
