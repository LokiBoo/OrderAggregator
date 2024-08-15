# Order Aggregator

This project is a simple order aggregator that combines multiple orders into a single order. It provides a way to aggregate orders from different sources and generate a consolidated order.

## Features

- Aggregates orders from multiple sources
- Generates a consolidated order
- Supports customization of aggregation logic
- Supports JWT authentication

## Usage

JWT token is required to access the API. 
Use this one: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3NTUyNTc1MDN9.qw1vx9JaWvXnVviClec_hyc1jzDKJ1gPVmDP_TNpa3o

## Customization

The Order Aggregator allows customization of the aggregation and export logic. 
You can implement your own logic by inheriting from the `IOrderStorageService`, `IOrdersExportService` and `IExportProcessor` interfaces.

## Perfomance

Was tested with JMeter and can handle up to 23000 requests per second (100 product ids each).

## Possible improvements

- Add more tests 
- Add more possibilities for export orders
- Add health check and metrics


