//
//  main.cpp
//  OpenGL Shadows
//
//  Created by CGIS on 05/12/16.
//  Copyright � 2016 CGIS. All rights reserved.
//

#define GLEW_STATIC

#include <iostream>
#include "glm/glm.hpp"//core glm functionality
#include "glm/gtc/matrix_transform.hpp"//glm extension for generating common transformation matrices
#include "glm/gtc/matrix_inverse.hpp"
#include "glm/gtc/type_ptr.hpp"
#include "GLEW/glew.h"
#include "GLFW/glfw3.h"
#include <string>
#include "Shader.hpp"
#include "SkyBox.hpp"
#include "Camera.hpp"
#define TINYOBJLOADER_IMPLEMENTATION

#include "Model3D.hpp"
#include "Mesh.hpp"



int glWindowWidth = 1820;
int glWindowHeight = 950;
int retina_width, retina_height;
int dir1,dir2,dir3,dir4,ceata;
GLFWwindow* glWindow = NULL;

const GLuint SHADOW_WIDTH = 8 * 2048, SHADOW_HEIGHT = 8 * 2048;
bool startP = 1;
int rotateL;
float fogDensity;
GLuint fogDensityLoc;
glm::mat4 model;
GLuint modelLoc;
glm::mat4 view;
GLuint viewLoc;
glm::mat4 projection;
GLuint projectionLoc;
glm::mat3 normalMatrix;
GLuint normalMatrixLoc;
glm::mat3 lightDirMatrix;
GLuint lightDirMatrixLoc;

glm::vec3 lightDir;
GLuint lightDirLoc;
glm::vec3 lightColor;
GLuint lightColorLoc;

//skybox
gps::SkyBox mySkyBox;
gps::Shader skyboxShader;
std::vector<const GLchar*> faces;

gps::Camera myCamera(glm::vec3(-200.0f, 100.0f, -200.0f), glm::vec3(0.0f, 0.0f, 0.0f));
GLfloat cameraSpeed = 1.5f;

bool pressedKeys[1024];
GLfloat angle;
GLfloat lightAngle;

gps::Model3D myModel;
gps::Model3D ground;
gps::Model3D rocketLauncher;
gps::Model3D lightCube;
gps::Model3D Plane;
gps::Model3D Elice;
gps::Model3D Buildings;
gps::Model3D Robot;
gps::Model3D Moon;
gps::Model3D LightPost;
glm::mat4 modelElice;
float PlaneAngle;
float EliceAngle;
glm::mat4 modelPlane;
gps::Shader myCustomShader;
gps::Shader lightShader;
gps::Shader depthMapShader;

GLuint shadowMapFBO;
GLuint depthMapTexture;
GLfloat yaw = 50.0f;
GLfloat pitch = 0.0f;
GLfloat lastX = glWindowWidth / 2.0f;
GLfloat lastY = glWindowHeight / 2.0f;
bool firstMouse;

GLenum glCheckError_(const char *file, int line)
{
	GLenum errorCode;
	while ((errorCode = glGetError()) != GL_NO_ERROR)
	{
		std::string error;
		switch (errorCode)
		{
		case GL_INVALID_ENUM:                  error = "INVALID_ENUM"; break;
		case GL_INVALID_VALUE:                 error = "INVALID_VALUE"; break;
		case GL_INVALID_OPERATION:             error = "INVALID_OPERATION"; break;
		case GL_STACK_OVERFLOW:                error = "STACK_OVERFLOW"; break;
		case GL_STACK_UNDERFLOW:               error = "STACK_UNDERFLOW"; break;
		case GL_OUT_OF_MEMORY:                 error = "OUT_OF_MEMORY"; break;
		case GL_INVALID_FRAMEBUFFER_OPERATION: error = "INVALID_FRAMEBUFFER_OPERATION"; break;
		}
		std::cout << error << " | " << file << " (" << line << ")" << std::endl;
	}
	return errorCode;
}
#define glCheckError() glCheckError_(__FILE__, __LINE__)

void windowResizeCallback(GLFWwindow* window, int width, int height)
{
	fprintf(stdout, "window resized to width: %d , and height: %d\n", width, height);
	//TODO
	//for RETINA display
	glfwGetFramebufferSize(glWindow, &retina_width, &retina_height);

	myCustomShader.useShaderProgram();

	//set projection matrix
	glm::mat4 projection = glm::perspective(glm::radians(45.0f), (float)retina_width / (float)retina_height, 0.1f, 1000.0f);
	//send matrix data to shader
	GLint projLoc = glGetUniformLocation(myCustomShader.shaderProgram, "projection");
	glUniformMatrix4fv(projLoc, 1, GL_FALSE, glm::value_ptr(projection));

	lightShader.useShaderProgram();

	glUniformMatrix4fv(glGetUniformLocation(lightShader.shaderProgram, "projection"), 1, GL_FALSE, glm::value_ptr(projection));

	//set Viewport transform
	glViewport(0, 0, retina_width, retina_height);
}

void keyboardCallback(GLFWwindow* window, int key, int scancode, int action, int mode)
{
	if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS)
		glfwSetWindowShouldClose(window, GL_TRUE);

	if (key >= 0 && key < 1024)
	{
		if (action == GLFW_PRESS)
			pressedKeys[key] = true;
		else if (action == GLFW_RELEASE)
			pressedKeys[key] = false;
	}
}

void mouseCallback(GLFWwindow* window, double xpos, double ypos)
{
	{
		if (firstMouse)
		{
			lastX = (GLfloat)xpos;
			lastY = (GLfloat)ypos;
			firstMouse = false;
		}

		GLfloat xoffset = (GLfloat)xpos - lastX;
		GLfloat yoffset = lastY - (GLfloat)ypos;
		lastX = (GLfloat)xpos;
		lastY = (GLfloat)ypos;

		GLfloat sensitivity = 0.5f;
		xoffset *= sensitivity;
		yoffset *= sensitivity;

		yaw += xoffset;
		pitch += yoffset;


		myCamera.rotate(pitch, yaw);
	}
}

void processMovement()
{
	if (pressedKeys[GLFW_KEY_P]) {
		startP = false;
		dir1 = 1;
		myCamera=gps::Camera (glm::vec3(0.0f, 50.0f, -50.0f), glm::vec3(0.0f, 0.0f, 0.0f));
	}
	if (pressedKeys[GLFW_KEY_Q]) {
		angle += 0.1f;
		if (angle > 360.0f)
			angle -= 360.0f;
	}

	if (pressedKeys[GLFW_KEY_E]) {
		angle -= 0.1f;
		if (angle < 0.0f)
			angle += 360.0f;
	}

	if (pressedKeys[GLFW_KEY_W]) {
		myCamera.move(gps::MOVE_FORWARD, cameraSpeed);
	}

	if (pressedKeys[GLFW_KEY_S]) {
		myCamera.move(gps::MOVE_BACKWARD, cameraSpeed);
	}

	if (pressedKeys[GLFW_KEY_A]) {
		myCamera.move(gps::MOVE_LEFT, cameraSpeed);
	}

	if (pressedKeys[GLFW_KEY_D]) {
		myCamera.move(gps::MOVE_RIGHT, cameraSpeed);
	}

	if (pressedKeys[GLFW_KEY_J]) {

		lightAngle += 1.5f;
		if (lightAngle > 360.0f)
			lightAngle -= 360.0f;
		glm::vec3 lightDirTr = glm::vec3(glm::rotate(glm::mat4(1.0f), glm::radians(lightAngle), glm::vec3(0.0f, 1.0f, 0.0f)) * glm::vec4(lightDir, 1.0f));
		myCustomShader.useShaderProgram();
		glUniform3fv(lightDirLoc, 1, glm::value_ptr(lightDirTr));
	}

	if (pressedKeys[GLFW_KEY_L]) {
		lightAngle -= 1.5f;
		if (lightAngle < 0.0f)
			lightAngle += 360.0f;
		glm::vec3 lightDirTr = glm::vec3(glm::rotate(glm::mat4(1.0f), glm::radians(lightAngle), glm::vec3(0.0f, 1.0f, 0.0f)) * glm::vec4(lightDir, 1.0f));
		myCustomShader.useShaderProgram();
		glUniform3fv(lightDirLoc, 1, glm::value_ptr(lightDirTr));
	}
	if (pressedKeys[GLFW_KEY_Z])//line
	{
		glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
	}

	if (pressedKeys[GLFW_KEY_X])//normal
	{
		glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
	}

	if (pressedKeys[GLFW_KEY_C])//point
	{
		glPolygonMode(GL_FRONT_AND_BACK, GL_POINT);
	}
	if (pressedKeys[GLFW_KEY_F])//fog on
	{
		ceata = 1;
	}
	if (pressedKeys[GLFW_KEY_G])//fog off
	{
		ceata = 0;
	}
}

bool initOpenGLWindow()
{
	if (!glfwInit()) {
		fprintf(stderr, "ERROR: could not start GLFW3\n");
		return false;
	}

	//for Mac OS X
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 4);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 1);
	glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

	glWindow = glfwCreateWindow(glWindowWidth, glWindowHeight, "OpenGL Shader Example", NULL, NULL);
	if (!glWindow) {
		fprintf(stderr, "ERROR: could not open window with GLFW3\n");
		glfwTerminate();
		return false;
	}

	glfwSetWindowSizeCallback(glWindow, windowResizeCallback);
	glfwMakeContextCurrent(glWindow);

	glfwWindowHint(GLFW_SAMPLES, 4);

	// start GLEW extension handler
	glewExperimental = GL_TRUE;
	glewInit();

	// get version info
	const GLubyte* renderer = glGetString(GL_RENDERER); // get renderer string
	const GLubyte* version = glGetString(GL_VERSION); // version as a string
	printf("Renderer: %s\n", renderer);
	printf("OpenGL version supported %s\n", version);

	//for RETINA display
	glfwGetFramebufferSize(glWindow, &retina_width, &retina_height);

	glfwSetKeyCallback(glWindow, keyboardCallback);
	glfwSetCursorPosCallback(glWindow, mouseCallback);
	//glfwSetInputMode(glWindow, GLFW_CURSOR, GLFW_CURSOR_DISABLED);

	return true;
}

void initOpenGLState()
{
	glClearColor(0.3f, 0.3f, 0.3f, 1.0f);
	glViewport(0, 0, retina_width, retina_height);

	glEnable(GL_DEPTH_TEST); // enable depth-testing
	glDepthFunc(GL_LESS); // depth-testing interprets a smaller value as "closer"
	//glEnable(GL_CULL_FACE); // cull face
	glCullFace(GL_BACK); // cull back face
	glFrontFace(GL_CCW); // GL_CCW for counter clock-wise
}

void initFBOs()
{
	//generate FBO ID
	glGenFramebuffers(1, &shadowMapFBO);

	//create depth texture for FBO
	glGenTextures(1, &depthMapTexture);
	glBindTexture(GL_TEXTURE_2D, depthMapTexture);
	glTexImage2D(GL_TEXTURE_2D, 0, GL_DEPTH_COMPONENT,
		SHADOW_WIDTH, SHADOW_HEIGHT, 0, GL_DEPTH_COMPONENT, GL_FLOAT, NULL);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);

	//attach texture to FBO
	glBindFramebuffer(GL_FRAMEBUFFER, shadowMapFBO);
	glFramebufferTexture2D(GL_FRAMEBUFFER, GL_DEPTH_ATTACHMENT, GL_TEXTURE_2D, depthMapTexture, 0);
	glDrawBuffer(GL_NONE);
	glReadBuffer(GL_NONE);
	glBindFramebuffer(GL_FRAMEBUFFER, 0);
}

glm::mat4 computeLightSpaceTrMatrix()
{
	const GLfloat near_plane = 1.0f, far_plane = 1000.0f;
	glm::mat4 lightProjection = glm::ortho(-200.0f, 200.0f, -200.0f, 200.0f, near_plane, far_plane);

	glm::vec3 lightDirTr = glm::vec3(glm::rotate(glm::mat4(1.0f), glm::radians(lightAngle), glm::vec3(0.0f, 1.0f, 0.0f)) * glm::vec4(lightDir, 1.0f));
	glm::mat4 lightView = glm::lookAt(lightDirTr, myCamera.getCameraTarget(), glm::vec3(0.0f, 1.0f, 0.0f));

	return lightProjection * lightView;
}

void initModels()
{
	myModel = gps::Model3D("objects/nanosuit/nanosuit.obj", "objects/nanosuit/");
	ground = gps::Model3D("objects/ground/ground.obj", "objects/ground/");
	lightCube = gps::Model3D("objects/cube/cube.obj", "objects/cube/");
	rocketLauncher = gps::Model3D("objects/rocketLauncher/rocketLauncher.obj", "objects/rocketLauncher/");
	Plane = gps::Model3D("objects/Plane/Plane.obj", "objects/Plane/");
	Elice = gps::Model3D("objects/Elice/Elice.obj", "objects/Elice/");
	Moon = gps::Model3D("objects/Moon/Moon.obj", "objects/Moon/");
	Buildings = gps::Model3D("objects/Building/Buildings.obj", "objects/Building/");
	Robot = gps::Model3D("objects/Robot/Robot.obj", "objects/Robot/");
	LightPost = gps::Model3D("objects/LightPost/LightPost.obj", "objects/LightPost/");
}

void initShaders()
{
	myCustomShader.loadShader("shaders/shaderStart.vert", "shaders/shaderStart.frag");
	lightShader.loadShader("shaders/lightCube.vert", "shaders/lightCube.frag");
	depthMapShader.loadShader("shaders/simpleDepthMap.vert", "shaders/simpleDepthMap.frag");
}

void initUniforms()
{
	myCustomShader.useShaderProgram();

	modelLoc = glGetUniformLocation(myCustomShader.shaderProgram, "model");

	viewLoc = glGetUniformLocation(myCustomShader.shaderProgram, "view");

	normalMatrixLoc = glGetUniformLocation(myCustomShader.shaderProgram, "normalMatrix");

	lightDirMatrixLoc = glGetUniformLocation(myCustomShader.shaderProgram, "lightDirMatrix");

	projection = glm::perspective(glm::radians(45.0f), (float)retina_width / (float)retina_height, 0.1f, 1000.0f);
	projectionLoc = glGetUniformLocation(myCustomShader.shaderProgram, "projection");
	glUniformMatrix4fv(projectionLoc, 1, GL_FALSE, glm::value_ptr(projection));

	//set the light direction (direction towards the light)
	lightDir = glm::vec3(230.8f, 233.4f, -324.5f);
	lightDirLoc = glGetUniformLocation(myCustomShader.shaderProgram, "lightDir");
	glUniform3fv(lightDirLoc, 1, glm::value_ptr(lightDir));

	//set light color
	lightColor = glm::vec3(1.0f, 1.0f, 1.0f)*0.4f; //white light
	lightColorLoc = glGetUniformLocation(myCustomShader.shaderProgram, "lightColor");
	glUniform3fv(lightColorLoc, 1, glm::value_ptr(lightColor));

	lightShader.useShaderProgram();
	glUniformMatrix4fv(glGetUniformLocation(lightShader.shaderProgram, "projection"), 1, GL_FALSE, glm::value_ptr(projection));
}

void RotateElice(gps::Shader s)
{
	/*110.7 107.1 69.2 */
	EliceAngle += 40.0f;
	modelElice = glm::mat4(1.0f);
	modelElice = glm::rotate(modelElice, glm::radians(PlaneAngle), glm::vec3(0.0f, 1.0f, 0.0f));//aplicam rotatia deodata cu avionul
	modelElice = glm::translate(modelElice, glm::vec3(110.7f, 107.1f, -69.2f));// translatam inapoi elicea la avion
	modelElice = glm::rotate(modelElice, glm::radians(40.5f), glm::vec3(0.0f, 1.0f, 0.0f));//rotim inapoi inghiul Elicei fata de avion
	modelElice = glm::rotate(modelElice, glm::radians(EliceAngle), glm::vec3(0.0f, 0.0f, 1.0f));//rotim pe Z
	modelElice = glm::rotate(modelElice, glm::radians(-40.5f), glm::vec3(0.0f, 1.0f, 0.0f));//rotim unghiul la elice sa fie 0 grade pe Z
	modelElice = glm::translate(modelElice, glm::vec3(-110.7f, -107.1f, 69.2f));//translatam in centru elicea
	glUniformMatrix4fv(modelLoc, 1, GL_FALSE, glm::value_ptr(modelElice));
	Elice.Draw(s);
}

void renderScene()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	processMovement();

	//render the scene to the depth buffer (first pass)

	depthMapShader.useShaderProgram();

	glUniformMatrix4fv(glGetUniformLocation(depthMapShader.shaderProgram, "lightSpaceTrMatrix"),
		1,
		GL_FALSE,
		glm::value_ptr(computeLightSpaceTrMatrix()));

	glViewport(0, 0, SHADOW_WIDTH, SHADOW_HEIGHT);
	glBindFramebuffer(GL_FRAMEBUFFER, shadowMapFBO);
	glClear(GL_DEPTH_BUFFER_BIT);

	//create model matrix for nanosuit
	model = glm::rotate(glm::mat4(1.0f), glm::radians(angle), glm::vec3(0, 1, 0));
	//send model matrix to shader
	glUniformMatrix4fv(glGetUniformLocation(depthMapShader.shaderProgram, "model"),
		1,
		GL_FALSE,
		glm::value_ptr(model));

	myModel.Draw(depthMapShader);

	//create model matrix for ground
	model = glm::translate(glm::mat4(1.0f), glm::vec3(0.0f, 0.0f, 0.0f));
	//send model matrix to shader
	glUniformMatrix4fv(glGetUniformLocation(depthMapShader.shaderProgram, "model"),
		1,
		GL_FALSE,
		glm::value_ptr(model));

	LightPost.Draw(depthMapShader);
	Robot.Draw(depthMapShader);
	Buildings.Draw(depthMapShader);
	ground.Draw(depthMapShader);
	rocketLauncher.Draw(depthMapShader);
	model = glm::rotate(model, glm::radians(PlaneAngle), glm::vec3(0.0f, 1.0f, 0.0f));
	RotateElice(depthMapShader);
	glUniformMatrix4fv(glGetUniformLocation(depthMapShader.shaderProgram, "model"),
		1,
		GL_FALSE,
		glm::value_ptr(model));
	Plane.Draw(depthMapShader);

	glBindFramebuffer(GL_FRAMEBUFFER, 0);

	//render the scene (second pass)

	myCustomShader.useShaderProgram();

	//send lightSpace matrix to shader
	glUniformMatrix4fv(glGetUniformLocation(myCustomShader.shaderProgram, "lightSpaceTrMatrix"),
		1,
		GL_FALSE,
		glm::value_ptr(computeLightSpaceTrMatrix()));

	//send view matrix to shader
	view = myCamera.getViewMatrix();
	glUniformMatrix4fv(glGetUniformLocation(myCustomShader.shaderProgram, "view"),
		1,
		GL_FALSE,
		glm::value_ptr(view));

	//compute light direction transformation matrix
	lightDirMatrix = glm::mat3(glm::inverseTranspose(view));
	//send lightDir matrix data to shader
	glUniformMatrix3fv(lightDirMatrixLoc, 1, GL_FALSE, glm::value_ptr(lightDirMatrix));

	glViewport(0, 0, retina_width, retina_height);
	myCustomShader.useShaderProgram();

	//bind the depth map
	glActiveTexture(GL_TEXTURE3);
	glBindTexture(GL_TEXTURE_2D, depthMapTexture);
	glUniform1i(glGetUniformLocation(myCustomShader.shaderProgram, "shadowMap"), 3);

	//create model matrix for nanosuit
	model = glm::rotate(glm::mat4(1.0f), glm::radians(angle), glm::vec3(0, 1, 0));
	//send model matrix data to shader	
	glUniformMatrix4fv(modelLoc, 1, GL_FALSE, glm::value_ptr(model));

	//compute normal matrix
	normalMatrix = glm::mat3(glm::inverseTranspose(view*model));
	//send normal matrix data to shader
	glUniformMatrix3fv(normalMatrixLoc, 1, GL_FALSE, glm::value_ptr(normalMatrix));

	myModel.Draw(myCustomShader);

	//create model matrix for ground
	model = glm::translate(glm::mat4(1.0f), glm::vec3(0.0f, 0.0f, 0.0f));
	//send model matrix data to shader
	glUniformMatrix4fv(modelLoc, 1, GL_FALSE, glm::value_ptr(model));

	//create normal matrix
	normalMatrix = glm::mat3(glm::inverseTranspose(view*model));
	//send normal matrix data to shader
	glUniformMatrix3fv(normalMatrixLoc, 1, GL_FALSE, glm::value_ptr(normalMatrix));

	LightPost.Draw(myCustomShader);
	Buildings.Draw(myCustomShader);
	Robot.Draw(myCustomShader);
	Moon.Draw(myCustomShader);
	ground.Draw(myCustomShader);
	rocketLauncher.Draw(myCustomShader);
	modelPlane = glm::mat4(1.0f);
	PlaneAngle -= 1.0f;
	modelPlane = glm::rotate(modelPlane, glm::radians(PlaneAngle), glm::vec3(0.0f, 1.0f, 0.0f));
	RotateElice(myCustomShader);
	glUniformMatrix4fv(modelLoc, 1, GL_FALSE, glm::value_ptr(modelPlane));
	Plane.Draw(myCustomShader);

	model = glm::rotate(glm::mat4(1.0f), glm::radians(lightAngle), glm::vec3(0.0f, 1.0f, 0.0f));
	if (ceata == 1)
	{
		fogDensity = 0.003f;
		fogDensityLoc = glGetUniformLocation(myCustomShader.shaderProgram, "fogDensity");
		glUniform1f(fogDensityLoc, fogDensity);
	}
	else
	{
		fogDensity = 0.0f;
		fogDensityLoc = glGetUniformLocation(myCustomShader.shaderProgram, "fogDensity");
		glUniform1f(fogDensityLoc, fogDensity);
	}
	//animation camera
	if (startP == false) 
	{
		if (myCamera.getCameraPosition().z > -300.0f && dir1==1) 
			myCamera.move(gps::MOVE_BACKWARD, cameraSpeed);
		else if (myCamera.getCameraPosition().z <= -300.0f)
		{
			dir1 = 0, dir2 = 1;
			myCamera = gps::Camera(glm::vec3(0.0f, 50.0f, 50.0f), glm::vec3(0.0f, 0.0f, 0.0f));;
		}
		if (myCamera.getCameraPosition().z < 300.0f && dir2 == 1) 
			myCamera.move(gps::MOVE_BACKWARD, cameraSpeed);
		
		else if (myCamera.getCameraPosition().z >300.0f)
		{
			dir2 = 0, dir3 = 1;
			myCamera = gps::Camera(glm::vec3(-50.0f, 50.0f, 0.0f), glm::vec3(0.0f, 0.0f, 0.0f));;
		}
		if (myCamera.getCameraPosition().x > -300.0f && dir3 == 1) 
			myCamera.move(gps::MOVE_BACKWARD, cameraSpeed);
		
		else if (myCamera.getCameraPosition().x <= -300.0f)
		{
			dir3 = 0, dir4 = 1;
			myCamera = gps::Camera(glm::vec3(50.0f, 50.0f, 0.0f), glm::vec3(0.0f, 0.0f, 0.0f));;
		}
		if (myCamera.getCameraPosition().x < 300.0f && dir4 == 1) {
			myCamera.move(gps::MOVE_BACKWARD, cameraSpeed);
		}
		else if (myCamera.getCameraPosition().x >= 300.0f)
			dir4 = 0;
	}

	skyboxShader.useShaderProgram();
	mySkyBox.Draw(skyboxShader, view, projection);
}

int main(int argc, const char * argv[]) {

	initOpenGLWindow();
	initOpenGLState();
	initFBOs();
	initModels();
	initShaders();
	initUniforms();
	glCheckError();

	faces.push_back("textures/skybox/right.tga");
	faces.push_back("textures/skybox/left.tga");
	faces.push_back("textures/skybox/top.tga");
	faces.push_back("textures/skybox/bottom.tga");
	faces.push_back("textures/skybox/back.tga");
	faces.push_back("textures/skybox/front.tga");

	mySkyBox.Load(faces);
	skyboxShader.loadShader("shaders/skyboxShader.vert", "shaders/skyboxShader.frag");
	skyboxShader.useShaderProgram();
	view = myCamera.getViewMatrix();
	glUniformMatrix4fv(glGetUniformLocation(skyboxShader.shaderProgram, "view"), 1, GL_FALSE,
		glm::value_ptr(view));

	projection = glm::perspective(glm::radians(45.0f), (float)retina_width / (float)retina_height, 0.1f, 1000.0f);
	glUniformMatrix4fv(glGetUniformLocation(skyboxShader.shaderProgram, "projection"), 1, GL_FALSE,
		glm::value_ptr(projection));

	while (!glfwWindowShouldClose(glWindow)) {
		renderScene();

		glfwPollEvents();
		glfwSwapBuffers(glWindow);
	}

	//close GL context and any other GLFW resources
	glfwTerminate();

	return 0;
}
