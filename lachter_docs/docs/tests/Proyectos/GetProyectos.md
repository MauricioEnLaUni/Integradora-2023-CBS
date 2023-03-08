---
sidebar_position: 1.0
---

# GetProyectos!

**Tipo:** Consulta

**Fecha:** 08/03/2023  **Numero de test:** 1.0

**Descripcion:** Verificar si en la base de datos podemos realizar consultas para saber los datos de todos los proyectos existenetes.

**Requerimientos:** N/A

**Resultados:**
- id
    - timestamp
    - creationTime
- name 
- createAt
- closed

- payHistory
    - id
        - timestamp
        - creationTime
    - name
    - createdAt
    - closed
    - owner

- tasks
    - id
        - timestamp
        - creationTime
    - name
    - createdAt
    - closed
    - startDate
    - parent
        - timestamp
        - creationTime
    - subtasks
        - timestamp
        - creationTime
    - employeesAssigned
        - id
            - timestamp
            - creationTime
        - name
        - createdAt
        - closed
    - material
        - id
            - timestamp
            - creationTime
        - name
        - createdAt
        - closed
        - quatity
        - owner
        - brand
        - location
            - street
            - number
            - colony
            - postalCode
            - city
            - state
            - country
            - coordinates
                - latitude
                - longitude
        - status
        - boughFor
        - provider
    - address
        - street
        - number
        - colony
        - postalCode
        - city
        - state
        - country
        - coordinates
            - latitude
            - longitude


## Rechazada
**Nota:** Es rechazada ya que la base de datos no cuenta con informacion.

**Tester:** Victor Mendoza.