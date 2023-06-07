// Ruta del archivo de salida
var outputFilePath = @"C:\jmf\Archivo.txt";

// Obtener todas las tablas del modelo
var tables = Model.Tables;

// Crear un StringBuilder para almacenar la información
var stringBuilder = new System.Text.StringBuilder();

// Recorrer las tablas y sus columnas
foreach (var table in tables)
{
    // Agregar el nombre de la tabla al StringBuilder
    stringBuilder.AppendLine($"Tabla: {table.Name}");

     if (table.Partitions.Count > 0)
     {
         var queryPartition = table.Partitions[0].Query;
         stringBuilder.AppendLine($"Query: {queryPartition}");

     }
     stringBuilder.AppendLine($"--------Columnas-----");

    // Recorrer las columnas de la tabla
    foreach (var column in table.Columns)
    {
        // Agregar el nombre de la columna al StringBuilder
        stringBuilder.AppendLine($"- {column.Name}");
    }

    // Agregar una línea en blanco para separar las tablas
    stringBuilder.AppendLine();
}

// Guardar el contenido en el archivo de salida
System.IO.File.WriteAllText(outputFilePath, stringBuilder.ToString());

// Mostrar un mensaje de éxito
System.Diagnostics.Process.Start("notepad.exe", outputFilePath);
