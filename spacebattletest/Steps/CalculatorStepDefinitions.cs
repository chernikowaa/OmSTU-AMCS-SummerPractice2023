using FluentAssertions;
using System;
namespace Specflowproj.Steps;
using spacebattle;
using TechTalk.SpecFlow;

[Binding, Scope(Feature = "Космический бой")]
public class Space_Battle
{
     private SpaceShip ship =  new SpaceShip(); 
     private Exception except {get; set;}

     [Given(@"космический корабль находится в точке пространства с координатами \((.*), (.*)\)")]
     public void Coord(double a, double b)
     {
     ship.coord = new double[]{a,b};
     }

     [Given(@"имеет мгновенную скорость \((.*), (.*)\)")]
     public void Speed(double a, double b)
     {
     ship.speed = new double[]{a,b};
     }

     [Given(@"космический корабль, положение в пространстве которого невозможно определить")]
     public void CoordNaN()
     {
     ship.coord = new double[]{double.NaN,double.NaN};
     }

     [Given(@"скорость корабля определить невозможно")]
     public void SpeedNaN()
     {
     ship.speed = new double[]{double.NaN,double.NaN};
     }

     [Given(@"изменить положение в пространстве космического корабля невозможно")]
     public void CoordNot()
     {
     ship.coord = new double[]{double.NaN,double.NaN};
     }
     

     [When(@"происходит прямолинейное равномерное движение без деформации")]
     public void SpaceBattle()
     {
          try
          {
               ship.coord = spacebattle.SpaceShip.Move(ship.coord, ship.speed);
          }
          catch(ArgumentException e)
          {
               except = e;
          }
     }
     

     [Then(@"космический корабль перемещается в точку пространства с координатами \((.*), (.*)\)")]
     public void move(double expect1, double expect2)
     {
          double[] expected = {expect1, expect2};
          ship.coord.Should().ContainInOrder(expected);
     }

     [Then(@"возникает ошибка Exception")]
     public void Exeptions()
     {
          except.Should().BeOfType<System.ArgumentException>();
     }
}
