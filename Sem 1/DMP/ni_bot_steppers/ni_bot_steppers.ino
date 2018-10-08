/*
   1 means turn left 45*  -1
   2 means turn left 90
   3 means turn right 45*   -2
   4 means turn right 90
   5 means beeps  -3
   if not then go forward nr of steps (>=5)
*/
#include "Stepper.h"
#include "Nokia_5110.h"

#define RST A0
#define CE A1
#define DC A2
#define DIN A3
#define CLK A4

#define L1 47
#define L2 45
#define L3 43
#define L4 41

#define R1 35
#define R2 33
#define R3 31
#define R4 29
#define TR 6
#define EC 7
typedef byte display_mode;
display_mode INVERSE_VIDEO = 0xd;
display_mode ALL_SEGMENTS_ON = 0x9;
display_mode NORMAL = 0xc;
display_mode BLANK = 0x8;

Nokia_5110 lcd = Nokia_5110(RST, CE, DC, DIN, CLK);
Stepper left_stepper(48, L1, L2, L3, L4);
Stepper right_stepper(48, R1, R2, R3, R4);
int command;
char inChar;
String LCDstring;
long long lastCommand;

void buzz()
{
  analogWrite(2, 127);
  delay(100);
  analogWrite(2, 0);
  delay(100);
  analogWrite(2, 127);
  delay(100);
  analogWrite(2, 0);
  delay(100);
  analogWrite(2, 127);
  delay(100);
  analogWrite(2, 0);
  delay(100);
}

void setup() {

  pinMode(A5, OUTPUT);
  pinMode(A6, OUTPUT);
  pinMode(A7, OUTPUT);

  pinMode(TR, OUTPUT);
  pinMode(EC, INPUT);
  digitalWrite(A7, LOW);
  digitalWrite(A6, HIGH);
  digitalWrite(A5, HIGH);
  delay(100);
  lcd.setDisplayMode(NORMAL);
  lcd.setContrast(25); // 60 is the default value set by the driver

  lcd.println("INITIALIZING BOT");
  lcd.println("Self");
  lcd.println("Destruction");
  lcd.println("sequence");
  lcd.println("disabled");
  relax();

  for (int i = 0; i < 10000; i++)//bip
  {
    left_stepper.step(2);
    right_stepper.step(2);
    //delay(1);
  }
  relax();
  buzz();
  lcd.clear();

  lcd.println("Initializing");
  lcd.println("Wired and BT");
  lcd.println("Serial");
  lcd.println("Connection");
  Serial1.begin(9600);//bluetooth
  Serial.begin(115200);//serial
  delay(1000);
  lcd.clear();
  Serial.flush();
  Serial1.flush();
}


void updateLCD()
{
  lcd.clear();
  lcd.print(LCDstring);
}


bool checkSensor()
{
  digitalWrite(TR, LOW);
  delayMicroseconds(2);
  // Sets the trigPin on HIGH state for 10 micro seconds
  digitalWrite(TR, HIGH);
  delayMicroseconds(10);
  digitalWrite(TR, LOW);
  long long t = micros();
  double distance;
  while (digitalRead(EC) == LOW);
  while (digitalRead(EC) == HIGH)
  {
    if ( micros() - t > 3000) return false;
    distance = (micros() - t) * 0.034 / 2;
  }

  if (distance < 30)
  {

    LCDstring = "obstacle detected ";
    updateLCD();
    return true;
  }
  return false;
}

void moveF()
{
  if (checkSensor())
  {
    buzz();
    for (int j = 50; j; j--)
      moveR();
    while(Serial1.available())
      Serial1.read();
    return;
  }
    for (int i = 0; i < 2; i++)  //fw
    {
      left_stepper.step(-1);
      right_stepper.step(1);
      delay(10);
    }
}

void moveB()
{
  for (int i = 0; i <2 ; i++) //bw
  {
    left_stepper.step(1);
    right_stepper.step(-1);
    delay(10);
  }
}

void moveL()
{
  for (int i = 0; i < 2; i++) //left
  {
    left_stepper.step(1); //turn 45 left
    right_stepper.step(1);
    delay(10);
  }
}

void moveR()
{
  for (int i = 0; i < 2; i++) //right
  {
    left_stepper.step(-1);//turn 45 right
    right_stepper.step(-1);
    delay(10);
  }
}

void relax()
{
  digitalWrite(L1, LOW);
  digitalWrite(L2, LOW);
  digitalWrite(L3, LOW);
  digitalWrite(L4, LOW);
  digitalWrite(R1, LOW);
  digitalWrite(R2, LOW);
  digitalWrite(R3, LOW);
  digitalWrite(R4, LOW);

}

void serialEvent1() 
{
  inChar = (char)Serial1.read();
  if (inChar == 'F') LCDstring = "Moving forward", moveF();
  if (inChar == 'B') LCDstring = "Moving back", moveB();
  if (inChar == 'L') LCDstring = "Turning left", moveL();
  if (inChar == 'R') LCDstring = "Turning right", moveR();
  if (inChar == 'G') LCDstring = "front left", moveF(), moveL();
  if (inChar == 'I') LCDstring = "front right", moveF(), moveR();
  if (inChar == 'H') LCDstring = "back left", moveB(), moveL();
  if (inChar == 'J') LCDstring = "back right", moveB(), moveR();
  Serial1.flush();
  lastCommand = millis();
}

void loop()
{
  if (millis() - lastCommand > 200)
  {
    updateLCD();
    LCDstring = "Waiting for\nCommands\nOver BT serial";
    relax();
  }
}
