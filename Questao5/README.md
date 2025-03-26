# Questao5

## Vis�o Geral
Questao5 � uma aplica��o .NET 6 que lida com transa��es de contas banc�rias. Ela usa MediatR para lidar com solicita��es e respostas, e inclui reposit�rios para gerenciar idempot�ncia, movimentos de conta e contas banc�rias.

## Tecnologias
- .NET 6
- C# 10.0
- MediatR

## Estrutura do Projeto
- **Application/Handlers**: Cont�m os manipuladores de solicita��es.
- **Application/Commands/Requests**: Cont�m as classes de solicita��o.
- **Application/Commands/Responses**: Cont�m as classes de resposta.
- **Domain/Entities**: Cont�m as classes de entidade.
- **Domain/Repositories**: Cont�m as interfaces dos reposit�rios.

## Principais Classes e Interfaces
- **MovimentarContaCorrenteHandler**: Manipula as solicita��es de transa��es de contas banc�rias.
- **IIdempotanciaRepository**: Interface para o reposit�rio de idempot�ncia.
- **IMovimentoRepository**: Interface para o reposit�rio de movimentos.
- **IContaCorrenteRepository**: Interface para o reposit�rio de contas banc�rias.
- **MovimentarContaCorrenteRequest**: Classe de solicita��o para transa��es de conta.
- **MovimentarContaCorrenteReponse**: Classe de resposta para transa��es de conta.
- **ContaCorrenteEntity**: Classe de entidade para contas banc�rias.
- **MovimentoEntity**: Classe de entidade para movimentos de conta.
- **IdempotenciaEntity**: Classe de entidade para idempot�ncia.

## Primeiros Passos
1. Clone o reposit�rio.
2. Abra a solu��o no Visual Studio.
3. Restaure os pacotes NuGet.
4. Compile a solu��o.
5. Execute a aplica��o.

## Uso
Para lidar com uma transa��o de conta banc�ria, envie uma solicita��o para o `MovimentarContaCorrenteHandler` com os detalhes necess�rios, como n�mero da conta, valor e tipo de movimento (cr�dito ou d�bito).

### Exemplo de Solicita��o via POSTMAN
>>{
	"info": {
		"_postman_id": "1c826ec3-d632-4fc8-a4ba-eec8f4314fcd",
		"name": "amcom",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json",
		"_exporter_id": "1674119"
	},
	"item": [
		{
			"name": "Credit",
			"request": {
				"method": "POST",
				"header": [
					{
						"key": "accept",
						"value": "*/*"
					},
					{
						"key": "Content-Type",
						"value": "application/json"
					}
				],
				"body": {
					"mode": "raw",
					"raw": "{\n  \"idRequest\": \"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\n  \"accountNumber\": 743,\n  \"amount\": 1000,\n  \"typeMovement\": \"C\"\n}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:7140/api/v1/moviment",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7140",
					"path": [
						"api",
						"v1",
						"moviment"
					]
				}
			},
			"response": []
		},
		{
			"name": "Debit",
			"request": {
				"method": "POST",
				"header": [
					{
						"key": "accept",
						"value": "*/*"
					},
					{
						"key": "Content-Type",
						"value": "application/json"
					}
				],
				"body": {
					"mode": "raw",
					"raw": "{\n  \"idRequest\": \"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\n  \"accountNumber\": 743,\n  \"amount\": 1000,\n  \"typeMovement\": \"D\"\n}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "https://localhost:7140/api/v1/moviment",
					"protocol": "https",
					"host": [
						"localhost"
					],
					"port": "7140",
					"path": [
						"api",
						"v1",
						"moviment"
					]
				}
			},
			"response": []
		},
		{
			"name": "BuscarSaldo",
			"request": {
				"method": "GET",
				"header": []
			},
			"response": []
		}
	]
}

## Contribuindo
Contribui��es s�o bem-vindas! Por favor, abra uma issue ou envie um pull request.


