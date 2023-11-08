#include <Wire.h>
#include "MAX30105.h"

#include "heartRate.h"

MAX30105 poxSensor;

const byte avgMedian = 4;
byte avgBPM_Array[avgMedian];
byte arrayPos = 0;
long lastHeartBeat = 0;

float beatsPerMinute;
int avgBPM;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  Serial.println("Initialisierung.");

  if(!poxSensor.begin(Wire, I2C_SPEED_FAST))
  {
    Serial.println("Sensor MAX30102 nicht gefunden.");
    while (1);
  }
  Serial.println("Lege deinen Finger mit konstantem Druck auf den Sensor.");

  poxSensor.setup();
  poxSensor.setPulseAmplitudeRed(0X0A);

}

void loop() {
  // put your main code here, to run repeatedly:
  long irValue = poxSensor.getIR();

  if (checkForBeat(irValue) == true)
  {
    long delta = millis() - lastHeartBeat;
    lastHeartBeat = millis();

    beatsPerMinute = 60 / (delta / 1000.0);

    avgBPM_Array[arrayPos++] = (byte)beatsPerMinute;
    if(arrayPos == 4)
    {
      arrayPos = 0;
    }      

  avgBPM = (avgBPM_Array[0] + avgBPM_Array[1] + avgBPM_Array[2] + avgBPM_Array[3]) / avgMedian; 

  }
  Serial.print("IR=");
  Serial.print(irValue);
  Serial.print(", BPM=");
  Serial.print(avgBPM);

  Serial.println();
}
