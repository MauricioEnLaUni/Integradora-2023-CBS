---
sidebar_position: 1.0
---

# PostNewProyecto!

**Tipo:** Insercion

**Fecha:** 08/03/2023  **Numero de test:** 1.0

**Descripcion:** Verificar si en la base de datos podemos ingresar un nuevo proyecto.

**Requerimientos:**
- name

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

**Tester:** Victor Mendoza.