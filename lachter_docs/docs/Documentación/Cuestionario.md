# Cuestionario para levantamiento de requerimientos

### Objetivos

**¿Qué problema desea solucionar con esta solución tecnológica?**

Controlar los gastos de materiales y de mano de obra evitando desperdicios y tiempos muertos, en la construcción. Además una mejor administración con la toma de decisiones oportuna tomando en cuenta los reportes que arroje el sistema así como el diagrama de Gantt que es el esqueleto principal del sistema.

**¿Qué otras áreas de oportunidad existen en la empresa?**


**¿Qué tipo de solución tecnológica ha considerado para solucionar esta problemática? ¿Por qué considera que esta es una solución adecuada?**

Consideraron varios ERP y CRM gratuitos y de paga, como Odoo, Open CRM etc. además de otros de paga pero no se ajustaban completamente a los requerimientos del cliente. 

Clientes: Es inicialmente por requerimientos de un cliente, se pretende hacerlo genérico para ofrecerlo para diferentes clientes.

**¿Cómo se integraría la solución propuesta a sus procesos actuales?**

Se hará una migración mediante pruebas paralelas, observando los resultados de lo que se tiene actualmente con la aplicación.

**En esta área, ¿cómo se desarrollan los procesos actuales de su empresa?**

Actualmente se maneja a través de hojas de excel.
Departamento de compras realiza la adquisición, departamento de construcción pide lo necesario para trabajar y lo asigna como requiere y hay un departamento de finanzas que maneja una caja chica y las adquisiciones.


**¿Han explorado alguna otra solución tecnológica en esta área?**


**¿Cómo se verían modificados por nuestra solución tecnológica?**


**De su experiencia, ¿qué tipo de optimizaciones pueden aplicarse a estos procesos?**

Conexión con CONTPAQ.
https://www.contpaqi.com/contabilidad 

**En específico, ¿qué objetivos espera obtener con este proyecto? ¿Cómo medirá que se cumplan estos objetivos?**

Determinar con mayor precisión cuales son las ganancias que se dan en cada proyecto.

**¿Cómo lo estarían usando los usuarios finales?**

Administradores, contadores en PC.
Los usuarios finales en sus celulares, con cualquier tipo que pidan.
Registro con código QR y registros manuales.

**¿Qué medidas de seguridad requerimos?**

Autenticación OAuth.


## PWA

**¿Qué tipo de reportes requieren que el sistema emita?**

Diagrama de Gantt.
Lista de actividades por persona.
Total de costos de materiales de mano de obra, separado por periodo y obra.
Reporte de ganancias totales por periodo y obra.



### Requerimientos
**¿Cuáles son las funciones mínimas que debe cumplir el proyecto?**

Listado de actividades por persona.
Listado por obra.
Ganancias generadas por proyecto.

**Idealmente, ¿cuáles son las funciones que debe cumplir el proyecto?**

Todos los reportes establecidos.
Que el funcionamiento de todos los roles sea interactivo, basado en roles.
Manejo de grupos y equipos.
Manejo de Horas trabajadas.

**¿Quiénes serán los operadores o usuarios del sistema propuesto?**

Administradores, Jefe de Obra, empleados, sistemas(mantenimiento)

**¿Qué tipo de roles tomará cada uno de los usuarios del sistema?**

- Jefe → Administrador general
- Encargado de Obra → Administrador local
- Trabajador → Empleado - Puede aparecer en diferentes obras.

**¿Qué tipo de acciones podrá ejecutar cada uno de los usuarios del sistema?**
- Jefe → Manejo de credenciales, acceso de lectura a la base de datos completa. 
- Encargado de Obra → Todos los privilegios sobre las obras que maneje.
- Trabajador → Control de actividades, inicio y fin, materiales usados. Únicamente leen las tareas que tienen asignadas. 

**¿A través de qué dispositivos se dará esta interacción?**

Android, iPhone y Desktops.

**¿En qué tipo de ambiente se ejecutará el sistema?**

iOS, Linux, Windows

**¿Hay alguna necesidad especial que se deba atender en cuanto a la configuración o instalación del sistema a desarrollar?**

N / A.

**¿Con qué tipo de infraestructura cuentan para el sistema? (Red interna, Nube o niebla, Sistemas de IoT, etc.)**

Internet, todo en nube. Hosting privado.


**¿Con qué sistemas o APIs existentes se debe poder relacionar el sistema a desarrollar?**

Localización.
Clima


**Si la pregunta anterior no es ninguna, ¿cómo se llevará a cabo esta comunicación?**

Internet.
Credenciales por entregar.

**¿Con qué tipo de documentación cuentan para estos sistemas?**

No.

**¿Qué tipo de normas oficiales o manuales técnicos debemos seguir durante el desarrollo?**

ISO-10018 - Documentación.
27001 - Seguridad.

**¿Qué tipo de documentos legales, por ejemplo de privacidad será necesario?**

Contrato de Desarrollo de Software.
Contrato de Propiedad Intelectual.
Licencia Proprietaria.

**¿Será posible para nosotros presentarnos para recabar documentos? (Cuestionarios, Focus Groups, Work Experience)**


**¿Qué tipo de documentación requieren que se genere por parte del equipo de trabajo?**

Crear un manual de usuario y un manual técnico.

**¿Habrá oportunidad de llevar a cabo una inspección del lugar?**

Sí, ver si podemos agendar.
