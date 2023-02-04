#version 330 core
out vec4 FragColor;
uniform vec3 u_AmbientLightColor;

void main()
{
    FragColor = vec4(u_AmbientLightColor, 1);
}