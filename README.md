# VRDiary


VRDiary is a virtual reality Android prototype application to write your diary in a secure digital environment.
In combination with Google Daydream you can completely dive into your personal diary-writing environment.
The application is completely developed in Unity 3D with use of the Google Cardboard/Daydream SDK.

## The Application

In the application multiple users have their own virtual environment, locked with a passcode, in which they can write their diary entries.
In the following pictures two different environments, implemented in this prototype, are shown. 

![Environment 1](https://github.com/kedenk/vrdiary/blob/master/images/env_1.PNG?raw=true)
![Environment 2](https://github.com/kedenk/vrdiary/blob/master/images/env_2.PNG?raw=true)

Every environment has a panel/textfield in which the user can write the text/diary. 
With head movements the user can look around in his/her current environment. 
Currently, the environments are hard-coded for every user. 
But in further versions this can be done by a configuration. 
When the app is started the user is on a "Landing Screen", called Passcode Wall, shown in the following picture. 

![Environment 2](https://github.com/kedenk/vrdiary/blob/master/images/passcode_wall.PNG?raw=true)

This Passcode Wall prevents unauthorized access to the diary. 
With the correct inserted passcode the corresponding environment for the current user will be loaded.

## Input 

The application uses a Sony PlayStation 4 Controller as an input device. To be able to input text with the controller the a intuitive input mechanism is used, visualized by a the following input panel.

![Controller Panel](https://github.com/kedenk/vrdiary/blob/master/images/controller_panel.png?raw=true)

With the left analogue stick of the controller one category, arranged in 360 degree around the panel, can be selected. 
With the controller select buttons (Triangle, Circle, Cross and Square) the corresponding character can be written. 

![PlayStation 4 Controller](https://github.com/kedenk/vrdiary/blob/master/images/ps4_controller_button_mapping_1.png?raw=true)

For example, the selected category in the above image has the characters “I”, “L”, “J” and “K”. 
If this category is selected and the button "Triangle" is pressed, the character "I" is written to the diary. 
To toggle between capitals and lower case letters the right analogue stick “R3” can be pressed.

With the button “Share” and “Options” the written document for a specific day can be stored or loaded. 
The user can move between different days of his/her diary with the buttons “L2” and “R2”. 
The “PS” button is used for logout. 

## Limitations

The virtual reality diary prototype has some limitations we want to describe briefly here. 

A big issue is the Passcode Wall. 
In the current version all passcodes have a length of four characters.
Because of the limited amount of different characters (Triangle, Circle, Cross and Square) there is high chance that more than one user uses the same passcode. 
The fact, that the user is just identified by the passcode and not by a combination of username and passcode, there is a problem, if two users would have the same passcode. 
This results in conflict which environment should be loaded. 
In further versions it is planned to enable a login with username and a passcode of variable length. 

Another point that has some potential for improvement is the input. 
Writing text with the current controller panel and PlayStation 4 Controller works well, but of course not as fast as writing a text with a keyboard.
Therefore, some kind of autocompletion known from smartphone keyboards could be applied. 
This could speed up and ease writing for users. 

---

## Prerequisites

To use this application a Android Smartphone with the minimum Android version 7, Google Cardboard/Daydream and a PlayStation 4 Controller are needed. 
The PlayStation 4 Controller should be connected via Bluetooth to the Android Smartphone. 

The application itself can be build and installed on the smartphone with the Unity 3D IDE. 
During the development we observed, that the hardware of the used smartphone has a crucial impact to the performance of the application. 
Therefore, we recommend to use smartphone with powerful state of the art hardware.
Otherwise it can lead to delays of the controller input and very low frame rates what results in bad user experiences.
