# Bentley Makeover

A standalone SMAPI mod for **Stardew Valley 1.6+** that transforms your farmer into a custom aesthetic configuration using a single console command.

## Features
- Registers the console command: `bentley_makeover`
- Checks for active save states to prevent code crashes.
- Instantly configures:
  - **Hairstyle**: 15
  - **Hair Color**: Pastel Blonde (RGB: 255, 220, 130)
  - **Shirt**: Shirt And Tie `(S)1123`
  - **Pants**: Relaxed Fit Pants `(P)12`
  - **Hat**: Top Hat `(H)10`

## Build Instructions

### Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Stardew Valley 1.6 installed on your PC or Android build environment.

### Compiling Locally
1. Clone this repository to your computer.
2. Open your terminal or command prompt in the root repository folder.
3. Run the following command:
   ```bash
   dotnet build --configuration Release
   ```
4. The mod will automatically compile and package itself inside the `_releases` folder due to the custom `Directory.Build.props` structure. If your local Stardew Valley game path is standard, the build workflow will deploy it directly to your `Mods` folder.
