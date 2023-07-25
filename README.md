# Teste Acesso

## Descrição
O Teste Acesso é um projeto de integração com a API de Conta, que tem como objetivo realizar a execução de transferências.

## Índice
- [Visão Geral](#visão-geral)
- [Execuçãao](#execucao)

## Visão Geral
A proposta do Teste Acesso é fornecer uma resposta rápida e processamento assíncrono para transferências. Para alcançar esse objetivo, foi escolhido o uso de filas com o RabbitMQ devido à capacidade de separar as etapas de processamento e à resiliência oferecida pelos padrões de retry e redelivery. No entanto, algumas melhorias podem ser feitas neste estudo de caso, tais como:

1. Implementação de uma camada de cache para reduzir as consultas à API de Conta em um intervalo pré-definido de tempo.
2. Para reforçar a resiliência e suporte a falhas, é possível aplicar a estratégia de DLQs (Dead Letter Queues) para lidar com mensagens que não puderam ser processadas corretamente pelas filas de destino.
3. Estruturação dos logs em uma ferramenta de visualização facilitada, como o ELK (Elasticsearch, Logstash e Kibana).

Além dos itens mencionados acima, esse caso de uso proporcionou um aprofundamento no padrão SAGA, que poderia ser utilizado para o mesmo propósito em soluções mais robustas. Esse padrão permitiria o uso de filas e exchanges do RabbitMQ para coordenar as etapas de ação e compensação.

## Execucao
Para executar o projeto, basta utilizar o docker-compose na raiz da solução.
