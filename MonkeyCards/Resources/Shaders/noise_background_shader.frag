#version 330 core

uniform float time;
uniform vec2 resolution;

uniform float speed;
uniform float scale;

uniform vec3 color1;
uniform vec3 color2;
uniform vec3 color3;
uniform vec3 color4;
uniform vec3 color5;

uniform float threshold1;
uniform float threshold2;
uniform float threshold3;
uniform float threshold4;

out vec4 FragColor;

float noise(vec2 p) {
    return fract(sin(dot(p, vec2(12.9898, 78.233))) * 43758.5453);
}

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

float fractalNoise(vec2 p, float t) {
    float f = 0.0;
    f += 0.5000 * interpolatedNoise(p + vec2(sin(t * speed * 0.1), cos(t * speed * 0.1)) * 1.0);
    f += 0.2500 * interpolatedNoise(p * 2.0 + vec2(sin(t * speed * 0.2), cos(t * speed * 0.3)) * 1.5);
    f += 0.1250 * interpolatedNoise(p * 4.0 + vec2(sin(t * speed * 0.4), cos(t * speed * 0.6)) * 2.0);
    f += 0.0625 * interpolatedNoise(p * 8.0 + vec2(sin(t * speed * 0.8), cos(t * speed * 1.0)) * 3.0);
    return f / 0.9375;
}

void main() {
    vec2 uv = gl_FragCoord.xy / resolution.xy;
    vec2 p = uv * scale;

    float f = fractalNoise(p, time);

    vec3 color;
    if (f < threshold1) {
        color = color1;
    } else if (f < threshold2) {
        color = color2;
    } else if (f < threshold3) {
        color = color3;
    } else if (f < threshold4) {
        color = color4;
    }
    else {
        color = color5;
    }

    FragColor = vec4(color, 1.0); 
}