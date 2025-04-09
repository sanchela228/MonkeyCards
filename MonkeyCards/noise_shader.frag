uniform float time;
uniform vec2 resolution;

// Шум Перлина (упрощенная реализация)
float noise(vec2 p) {
    return fract(sin(dot(p, vec2(12.9898, 78.233))) * 43758.5453);
}

// Интерполяционный шум
float interpolatedNoise(vec2 p) {
    vec2 i = floor(p);
    vec2 f = fract(p);
    
    float a = noise(i);
    float b = noise(i + vec2(1.0, 0.0));
    float c = noise(i + vec2(0.0, 1.0));
    float d = noise(i + vec2(1.0, 1.0));
    
    vec2 u = f * f * (3.0 - 2.0 * f);
    return mix(a, b, u.x) + (c - a) * u.y * (1.0 - u.x) + (d - b) * u.x * u.y;
}

// Цветовая палитра (нефтяные разводы)
vec3 oilPalette(float t) {
    vec3 color = vec3(0.5);
    color = mix(color, vec3(0.2, 0.3, 0.8), smoothstep(0.0, 0.5, t));
    color = mix(color, vec3(0.8, 0.9, 0.3), smoothstep(0.3, 0.8, t));
    color = mix(color, vec3(0.9, 0.4, 0.1), smoothstep(0.6, 1.0, t));
    return color;
}

void main() {
    vec2 uv = gl_FragCoord.xy / resolution.xy;
    
    // Масштаб и анимация
    float scale = 3.0;
    vec2 p = uv * scale;
    p += time * 0.1;
    
    // Многослойный шум
    float f = 0.0;
    f += 0.5000 * interpolatedNoise(p);
    f += 0.2500 * interpolatedNoise(p * 2.0);
    f += 0.1250 * interpolatedNoise(p * 4.0);
    f += 0.0625 * interpolatedNoise(p * 8.0);
    f = f / 0.9375; // Нормализация
    
    // Добавляем волновой эффект
    f += sin(uv.y * 20.0 + time * 2.0) * 0.1;
    
    // Применяем цветовую палитру
    vec3 color = oilPalette(f);
    
    gl_FragColor = vec4(color, 1.0);
}