---
sidebar_position: 1.0
---

# PutProyecto!

**Tipo:** UpDate

**Fecha:** 08/03/2023  **Numero de test:** 1.0

**Descripcion:** Verificar si en la base de datos podemos modificar un proyecto ya existente.

**Requerimientos:**
- id
- name 
- tasks
    - id
    - name
    - createdAt
    - closed
    - startDate
    - parent
    - subtasks
    - employeesAssigned
        - id
        - name
        - createdAt
        - closed
    - material
        - id
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
    - closed
    - payHistory
        - id
        - name
        - createdAt
        - closed
        - owner

**Nota:** Esta informacion solo sera actualizada en la base de datos.

## Rechazada

**Tester:** Victor Mendoza.