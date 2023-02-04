#version 330 core
out vec4 FragColor;
in vec3 v_Normal;
in vec3 v_Position;

uniform vec3 u_LightPosition;
uniform vec3 u_CamPosition;
uniform vec3 u_AmbientLightColor;
uniform vec3 u_DiffuseLightColor;
uniform vec3 u_SpecularLightColor;
uniform float u_M;

void main()
{
    vec3 lightDir = normalize(u_LightPosition - v_Position);
    vec3 norm = normalize(v_Normal);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 reflectionVector = normalize(reflect(-lightDir, norm));
    float specularLightDot = max(dot(reflectionVector, normalize(u_CamPosition - v_Position)), 0.0);
    float specularLightParam = pow(specularLightDot, u_M);
    FragColor = vec4(u_AmbientLightColor + u_DiffuseLightColor * diff + u_SpecularLightColor * specularLightParam, 1);
}