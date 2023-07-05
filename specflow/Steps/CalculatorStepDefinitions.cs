using SquareEquationLib;
using TechTalk.SpecFlow;
using FluentAssertions;
using System;


namespace Specflowproj.Steps
{
    [Binding]
    public sealed class SquareEquationStepDefinitions
    {
       private double[] coef {get; set;}
       private double[] result {get; set;}
       private Exception except {get; set;} 

       [Given(@"Квадратное уравнение с коэффициентами \((.*), (.*), (.*)\)")]
       public void expect(string a, string b, string c)
       {
            this.coef = new double[3];
            string[] coefs = {a, b, c};
            
            for(var i = 0; i < 3; i++){
                if (coefs[i] == "Double.PositiveInfinity")
                    this.coef[i] = double.PositiveInfinity;
                else if (coefs[i] == "Double.NegativeInfinity")
                    this.coef[i] = double.NegativeInfinity;
                else if (coefs[i] == "NaN")
                    this.coef[i] = double.NaN;
                else
                    this.coef[i] = double.Parse(coefs[i]);
            }
       }
        
       [When(@"вычисляются корни квадратного уравнения")]
       public void SquareEquation()
       {
           try{
                this.result = SquareEquationLib.SquareEquation.Solve(coef[0], coef[1], coef[2]);
           }
           catch(ArgumentException e){
                except = e;
           }
       }

       [Then(@"квадратное уравнение имеет два корня \((.*), (.*)\) кратности один")]
       public void discriminant_more_zero(double expect1, double expect2)
       {
           double[] expected = {expect1, expect2};
           Array.Sort(expected);
           Array.Sort(this.result);

           result.Should().ContainInOrder(expected);
       }

       [Then(@"квадратное уравнение имеет один корень (.*) кратности два")]
       public void discriminant_equals_zero(double expected)
       {
           result[0].Should().Be(expected);
       }

       [Then(@"множество корней квадратного уравнения пустое")]
       public void discriminant_less_zero()
       {

            result.Length.Should().Be(0);
       }

       [Then(@"выбрасывается исключение ArgumentException")]
       public void Exeptions()
       {
            except.Should().BeOfType<System.ArgumentException>();
       }
    }
}