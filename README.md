# Sistema Senator
Sistema de gestión de reservaciones para los restaurantes del complejo turístico Senator, desarrollado en C#.

## Restaurantes
- Ember (Carnes) - 3 grupos
- Zao (Japonés) - 4 grupos
- Grappa (Italiano) - 2 grupos
- Larimar (Mariscos) - 3 grupos

## Funcionalidades
- Realizar reservación
- Eliminar reserva
- Ver disponibilidad por turno
- Imprimir listado por restaurante

## Ejemplo de uso
Se intento reservar a Veatris en Grappa Turno A pero estaba lleno, 
entonces se reservo en Grappa Turno B y se confirmo sin problema.
Algo muy importante es que si ingresas una persona y las reservaciones
del turno A y B estan llenas, en un grupo si intentas buscarlo te dira
que no se encuentra un registro con ese nombre ya que no se agrego porque
no habia cupo. (:
