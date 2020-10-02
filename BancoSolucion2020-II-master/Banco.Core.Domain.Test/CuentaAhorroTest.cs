using NUnit.Framework;
using System;

namespace Banco.Core.Domain.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        //Escenario: Valor de consignación negativo o cero 
        //H1: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el dinero.
        //Criterio de Aceptación:
        //1.2 El valor a abono no puede ser menor o igual a 0
        //Ejemplo
        //Dado El cliente tiene una cuenta de ahorro                                       //A =>Arrange /Preparación
        //Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0 , ciudad Valledupar                               
        //Cuando Va a consignar un valor menor o igual a cero  (0)                            //A =>Act = Acción
        //Entonces El sistema presentará el mensaje. “El valor a consignar es incorrecto”  //A => Assert => Validación
        [Test]
        public void NoPuedeConsignacionValorNegativoCuentaAhorroTest()
        {
            //Preparar
            var cuentaAhorro = new CuentaAhorro( numero: "10001", nombre:"Cuenta Ejemplo", ciudad:"Valledupar");
            //Acción
            var resultado=cuentaAhorro.Consignar(0, "Valledupar", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("El valor a consignar es incorrecto.", resultado);
        }


        //Escenario: Consignación Inicial Correcta
        //HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el dinero.
        //Criterio de Aceptación:
        //1.1 La consignación inicial debe ser mayor o igual a 50 mil pesos
        //1.3 El valor de la consignación se le adicionará al valor del saldo aumentará
        //Dado El cliente tiene una cuenta de ahorro
        //Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0
        //Cuando Va a consignar el valor inicial de 50 mil pesos
        //Entonces El sistema registrará la consignación
        //AND presentará el mensaje. “Su Nuevo Saldo es de $50.000,00 pesos m/c”.
        [Test]
        public void PuedeConsignacionInicialCorrectaCuentaAhorroTest()
        {
            //Preparar
            var cuentaAhorro = new CuentaAhorro(numero: "11001", nombre: "Cuenta Ejemplo2", ciudad: "Valledupar");
            //Acción
            var resultado = cuentaAhorro.Consignar(50000, "Valledupar", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("Su Nuevo Saldo es de $50,000.00 pesos m/c", resultado);
        }

        //Escenario: Consignación Inicial Incorrecta
        //HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el dinero.
        //Criterio de Aceptación:
        //1.1 La consignación inicial debe ser mayor o igual a 50 mil pesos
        //Dado El cliente tiene una cuenta de ahorro con
        //Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0
        //Cuando Va a consignar el valor inicial de $49.999 pesos
        //Entonces El sistema no registrará la consignación
        //AND presentará el mensaje. “El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos”. 

        [Test]
        public void NoPuedeConsignarMenosDeCincuentaMilPesosTest()
        {
            //Preparar
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            var resultado = cuentaAhorro.Consignar(49999, "Valledupar", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos", resultado);
        }

        [Test]
        public void AdicionaValorSaldoTest()
        {
            //Preparar
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            var resultado = cuentaAhorro.Consignar(70000, "Valledupar", DateTime.Now.Month + "-" + DateTime.Now.Year);
            var saldo = cuentaAhorro.Saldo;
            Console.WriteLine(cuentaAhorro.Saldo);
            //Verificacion
            Assert.AreEqual(70000m, saldo);
        }

        [Test]
        public void ConsignacionInterMunicipalTest()
        {
            //Preparar
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            var resultado = cuentaAhorro.Consignar(50000, "Barranquilla", DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("Su Nuevo Saldo es de $40,000.00 pesos m/c", resultado);

        }
        ///////////////////////////////////////
        //Pruebas Retiros
        //////////////////////////////////////
        [Test]
        public void RetirarSaldoCuentaTest()
        {
            //Preparar
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            cuentaAhorro.Consignar(100000, "Valledupar", DateTime.Now.Month + "-" + DateTime.Now.Year);
            var resultado = cuentaAhorro.Retirar(50000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("Su nuevo Saldo es de $50,000.00 pesos.", resultado);
        }

        [Test]
        public void RetiroSaldoMinimoCuentaTest()
        {
            //Preparar
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            cuentaAhorro.Consignar(500000, "Valledupar", DateTime.Now.Month + "-" + DateTime.Now.Year);
            cuentaAhorro.Retirar(490000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            var resultado = cuentaAhorro.Retirar(100, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("el saldo de la cuenta debe ser mayor de $20,000.00", resultado);
        }

        [Test]
        public void RetiroSinCostoTest()
        {
            //Preparar
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            cuentaAhorro.Consignar(100000, "Valledupar", DateTime.Now.Month + "-" + DateTime.Now.Year);
            cuentaAhorro.Retirar(10000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            cuentaAhorro.Retirar(10000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            var resultado = cuentaAhorro.Retirar(10000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("Su nuevo Saldo es de $70,000.00 pesos.", resultado);
        }

        [Test]
        public void RetiroConCostoTest()
        {
            //Preparar
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudad: "Valledupar");
            //Acción
            cuentaAhorro.Consignar(100000, "Valledupar", DateTime.Now.Month + "-" + DateTime.Now.Year);
            cuentaAhorro.Retirar(10000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            cuentaAhorro.Retirar(10000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            cuentaAhorro.Retirar(10000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            var resultado = cuentaAhorro.Retirar(10000, DateTime.Now.Month + "-" + DateTime.Now.Year);
            //Verificación
            Assert.AreEqual("Su nuevo Saldo es de $55,000.00 pesos.", resultado);
        }
    }
}