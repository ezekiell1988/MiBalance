export const environment = {
  production: true,
  apiUrl: 'http://localhost:5000/api/', // TODO: Cambiar en producción
  
  // Configuración de logs en PRODUCCIÓN
  logging: {
    enabled: false, // ❌ Deshabilitar logs en producción (excepto errors críticos)
    showTimestamp: false,
    showContext: false,
    useColors: false
  }
};
