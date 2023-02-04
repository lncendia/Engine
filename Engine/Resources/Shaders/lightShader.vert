#version 330 core
layout (location = 0) in vec3 a_Position;
layout (location = 1) in vec3 a_Normal;
out vec3 v_Normal;
out vec3 v_Position;

uniform mat4 u_MMatrix;
uniform mat4 u_VMatrix;
uniform mat4 u_PMatrix;
uniform mat3 u_NMatrix;

void main()
{
    v_Normal = a_Normal * u_NMatrix;
    vec4 position = vec4(a_Position, 1.0) * u_MMatrix;
    v_Position = vec3(position);
    gl_Position = position * u_VMatrix * u_PMatrix;
}