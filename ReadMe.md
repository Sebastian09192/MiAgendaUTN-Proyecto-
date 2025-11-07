# 🧭 1. Inicializar Git y crear el README.md
git init

# Crear archivo README.md con contenido completo
cat << 'EOF' > README.md
# 📅 MiAgendaUTN

**MiAgendaUTN** es una aplicación móvil desarrollada con **.NET MAUI**, pensada para estudiantes de la **UTN (Universidad Técnica Nacional)**.  
Su objetivo es permitir la gestión sencilla de tareas, recordatorios y pendientes académicos desde un solo lugar.

---

## 🚀 Características principales

- ✅ **Gestión de tareas:** crear, editar y eliminar tareas fácilmente.  
- 🗓️ **Visualización de pendientes y completadas.**  
- 💾 **Almacenamiento local:** mediante SQLite, sin conexión a internet.  
- 🔁 **Sincronización opcional:** exporta e importa tus tareas en formato JSON.  
- 🎨 **Diseño adaptable:** interfaz ligera, moderna y compatible con Android y Windows.  
- 🧠 **Arquitectura limpia:** MVVM + CommunityToolkit.Mvvm.

---

## 🛠️ Tecnologías utilizadas

- [.NET MAUI](https://learn.microsoft.com/dotnet/maui)  
- [SQLite-net-pcl](https://www.nuget.org/packages/sqlite-net-pcl)  
- [CommunityToolkit.Mvvm](https://www.nuget.org/packages/CommunityToolkit.Mvvm/)  
- [System.Text.Json](https://learn.microsoft.com/dotnet/api/system.text.json)

---

## 📁 Estructura del proyecto

MiAgendaUTN/
├── Models/ # Clases de datos (Tarea, etc.)
├── ViewModels/ # Lógica de presentación (MVVM)
├── Views/ # Páginas XAML
├── Services/ # Servicios (Base de datos, JSON)
├── Helpers/ # Convertidores y utilidades
├── App.xaml # Recursos globales y estilos
├── AppShell.xaml # Navegación principal
└── MauiProgram.cs # Configuración de dependencias

---

## ⚙️ Configuración y ejecución

### 1️⃣ Requisitos previos
- [Visual Studio 2022 o superior](https://visualstudio.microsoft.com/)
- Carga de trabajo **.NET Multi-platform App UI (MAUI)**
- Android SDK instalado
- Git (opcional, para clonar el repositorio)

### 2️⃣ Clonar el proyecto

git clone https://github.com/TU_USUARIO/MiAgendaUTN.git
cd MiAgendaUTN

### 3️⃣ Restaurar dependencias

Abre el proyecto en Visual Studio y espera a que se restauren los paquetes NuGet.

4️⃣ Ejecutar la aplicación

Selecciona una de las siguientes opciones:

## ▶️ Android Emulator

## 🖥️ Windows Machine

Presiona F5 para compilar y ejecutar.