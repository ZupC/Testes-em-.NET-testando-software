using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class PatioTestes: IDisposable
    {
        private Patio estacionamento;
        private Veiculo veiculo;
        private Operador operador;
        public ITestOutputHelper SaidaConsoleTeste;

        public PatioTestes(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado .");
            veiculo = new Veiculo();
            estacionamento = new Patio();
            operador = new Operador();
        }

        [Fact]
        public void ValidaFaturamentoDoEstacionamentoCom1Veiculo()
        {
            //Arrange
            operador.Nome = "Bento";
            estacionamento.OperadorPatio = operador;

            veiculo.Proprietario = "Cairo";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Prata";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "asd-9999";

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);
            //Act
            double faturamento = estacionamento.TotalFaturado();
            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("Cairo","asd-8888","preto","chevette")]
        [InlineData("Salame","dsa-7777","vermelho","gol com escada em cima")]
        [InlineData("Moon","qwe-6666","verde","relampago marquinhos")]
        public void ValidaFaturamentoDoEstacionamentoComVariosVeiculos(string propietario, string placa, string cor, string modelo)
        {
            //Arrange
            operador.Nome = "Bento";
            estacionamento.OperadorPatio = operador;

            veiculo.Proprietario = propietario;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);
            //Act
            double faturamento = estacionamento.TotalFaturado();
            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("Cairo", "asd-8888", "preto", "chevette")]
        [InlineData("Salame", "dsa-7777", "vermelho", "gol com escada em cima")]
        [InlineData("Moon", "qwe-6666", "verde", "relampago marquinhos")]
        public void LocalizaVeiculoNoPatioPorIdTicket(string propietario, string placa, string cor, string modelo)
        {
            //Arrange
            operador.Nome = "Bento";
            estacionamento.OperadorPatio = operador;

            veiculo.Proprietario = propietario;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            //Act
            var consultado = estacionamento.PesquisaVeiculo(veiculo.IdTicket);
            //Assert
            Assert.Contains("### Ticket estacionamento alura ###", consultado.Ticket);
        }

        [Fact]
        public void AlterarDadosDoProprioVeiculo()
        {
            //Arrange
            operador.Nome = "Bento";
            estacionamento.OperadorPatio = operador;

            veiculo.Proprietario = "Cairo";
            veiculo.Placa = "asd-9999";
            veiculo.Cor = "Prata";
            veiculo.Modelo = "Fusca";
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = "Cairo";
            veiculoAlterado.Placa = "asd-9999";
            veiculoAlterado.Cor = "Preto";
            veiculoAlterado.Modelo = "Fusca";
            veiculoAlterado.HoraEntrada = veiculo.HoraEntrada;

            //Act
            Veiculo alterado = estacionamento.AlterarDadosVeiculo(veiculoAlterado);
            //Assert
            Assert.Equal(alterado.Cor, veiculoAlterado.Cor);
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose invocado .");
        }
    }
}
