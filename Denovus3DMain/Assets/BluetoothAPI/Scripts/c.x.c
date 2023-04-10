#include <ESP8266WiFi.h>
#ifndef STASSID
#define STASSID "Elieee"//"-NRK HOME-"// "Elieee" //input //Nakhle... 
#define STAPSK "lello888"//"nrk@04543382"
#endif

const int pinoutA=13;//D7
const int pinoutB=15;//D8
const char *ssid = STASSID;
const char *password = STAPSK;

WiFiClient client;

const char * apiToken = "K57EOE0PXJNDSH1"; //input
const char * appToken = "652C4G36KQLIY3A";//fixed
const char * deviceKey = "ARDNO"; //fixed, output
const char * host = "fypautomation.ddns.net";
const int port = 9909;

bool connected;
void setup()
{  
  // initialize digital pin LED_BUILTIN as an output.
  pinMode(pinoutA, OUTPUT);
   pinMode(pinoutB, OUTPUT);
    Serial.begin(115200);

    // We start by connecting to a WiFi network

    Serial.println();
    Serial.println();
    Serial.print("Connecting to ");
    Serial.println(ssid);

    /* Explicitly set the ESP8266 to   be a WiFi-client, otherwise, it by default,
     would try to act as both a client and an access-point and could cause
     network-issues with your other WiFi-devices on your WiFi-network. */
    WiFi.mode(WIFI_STA);
    WiFi.begin(ssid, password);

    while (WiFi.status() != WL_CONNECTED)
    {
        delay(500);
        Serial.print(".");
    }

    Serial.println("");
    Serial.println("WiFi connected");
    Serial.println("IP address: ");
    Serial.println(WiFi.localIP());
  
    connected = false;
    while(!connected)
    {

      if (!client.connect(host, port)){
        Serial.println("Cannot connect to the cloud");
        continue;;
      }
          
     int length = 36;
      char *authMsg = new char[40];
      memset(authMsg, 0, 40 * sizeof(char));
      authMsg[0] = 0;
      authMsg[3] = (char)length; // fixed value
      authMsg[4] = 'D'; // fixed value, 1 2 3 4
      for (int i = 0; i < 15; i++)
          authMsg[i + 5] = apiToken[i];
      for (int i = 0; i < 15; i++)
          authMsg[i + 20] = appToken[i];
      for (int i = 0; i < 5; i++)
          authMsg[i + 35] = deviceKey[i];
      client.write(authMsg, 40);
      client.flush();
  
      Serial.println("Sending auth message...");
  
      delete[] authMsg;
  
      byte count = 0;
      while (!client.available())
      {
          delay(100);
          count++;
          if (count >= 30)
          {
              client.stop();
              continue;
          }
      }
  
      byte x = client.read();
      if (x == 1)
          connected = true;
      else
          client.stop();
    }

    // sending Status Off to the android application for once only, this is to mark that the initil status of the lamp is OFFs


    
}


void loop()
{ 
  Serial.println("LOOP SECTION");
   /*if (digitalRead(pinoutA)==HIGH)
    {
  char * message ="LON"; 
    sendtocloud(message,3);} 
    else
    {
        char  * message ="LOFF"; 
    sendtocloud(message,4);
      }
  delay(5000);

 if (digitalRead(pinoutB)==HIGH)
    {
  char * message ="TVON"; 
    sendtocloud(message,4);} 
    else
    {
        char  * message ="TVOFF"; 
    sendtocloud(message,5);} */
    
   
  /*digitalWrite(pinoutA, HIGH);  
delay(7000);

  // chil hol l tahet wa2ta badak tjarb tekhd data mn rita mn hon
    char  message []="LOFF"; 
    sendtocloud(message, sizeof(message));
   //char * message = new char [6];
   //message[0]='L';message[1]='E';message[2]='D';message[3]=' ';message[4]='O';message[5]='N';
  //sendtocloud(message, sizeof(message)); 
// la  hon  chilun  */

   //sendtocloud(payload, l);
  if(client.available() > 4){
    int l = client.read();
    l = client.read();
    l=client.read();
    l=client.read();

    char * payload = new char[l];
    int i=0;
    while(i<l)
    {
      delay(10);
      if(!client.available())
        continue;
       payload[i] = client.read();
       i++;
    }

    char msgType = payload[0];
    char * source = new char[5];
    source[0]=payload[1];
    source[1]=payload[2];
    source[2]=payload[3];
    source[3]=payload[4];
    source[4]=payload[5];//......
char * data= new char[l-6];

for (int i=0; i<l-6;i++)
  { data[i] = payload[6+i];
Serial.print(data[i]); }

Serial.println(" ");

//String cmd= String(data);


if (data[0]=='L' && data[1]=='O' && data[2]=='N')    
     {Control("LON");}
  
else if (data[0]=='L' && data[1]=='O' && data[2]=='F' && data[3]=='F'){ Control("LOFF");}  

else if(data[0]=='T' && data [1]=='V' && data[2]=='O' && data[3]=='N') {Control("TVON");}

else if (data[0]=='T' && data [1]=='V' && data[2]=='O' && data[3]=='F' && data[4]=='F') {Control("TVOFF");}

else if (data[0]=='L' && data[1]=='S') { CheckStatus("LED"); }

else if (data[0]=='T' && data[1]=='S') { CheckStatus("TV");}

else {Serial.println("Command Error");}

  delay(1000);        // delay in between reads for stability
}

delay(1000);


//CONSTRUCT NEW MSG
//MSG TYPE://DATA 1 
//ERROR 2  LENGTH(4) MSGTYPE(1) ERRORCODE (4) ERROR MSG(L-5)
//ECHO 3
//BORADCAST 4
//EMPTY 5
    
  }
  ////




void sendtocloud(char* data, int l)
{
   /*char x[4] = {0};
    x[3] = l;
   client.write(x, 4);client.flush();
   client.write(data, l);client.flush();*/
unsigned int size=l+10;

     char x[size];
     x[0]=0; x[1]=0; x[2]=0;
   x[3]=6+l;
   x[4]=1;//Message Type Data

   strcpy(x+5, "RITAA");
//x[5]='A';x[6]='R';x[7]='D';x[8]='N';x[9]='O';//device Key
                                            //x[10]='L',x[11]='O',x[12]='N';
for (int i=10; i<=l+10;i++) //l-6 if im recieving from app
  { x[i]=data[i-10];
  Serial.print(data[i-10]);}
  Serial.println(" ");
client.write(x,l+10); // sending headers + data
client.flush();

}

void Control(String command)
{ 
 String s=command;
  if (s=="LON") {
    digitalWrite(pinoutA, HIGH);   // turn the Lamp on (HIGH is the voltage level)
    char  message []="LED ON"; 
    sendtocloud(message, sizeof(message));}

  else if(s=="LOFF")
  {
    digitalWrite(pinoutA, LOW); // turn the Lamp OFF (Low is the voltage level)
    char message []="LED OFF";
    sendtocloud(message, sizeof(message));}

 else if (s=="TVON") 
 {
   digitalWrite(pinoutB, HIGH); // turn the TV ON (Low is the voltage level)
   char message []="TV ON";
   sendtocloud(message, sizeof(message)); }
 
  else if (s=="TVOFF") 
 {
   digitalWrite(pinoutB, LOW); // turn the TV OFF (Low is the voltage level)
   char message []="TV OFF";
   sendtocloud(message, sizeof(message)); }
 
   else{ Serial.println("INVALID LED COMMAND");}
}

void CheckStatus(String component){
  
  if (component=="LED")
        {
          if (digitalRead(pinoutA)==HIGH)
               {
                 char * message ="LON"; 
                 sendtocloud(message,3);
                 } 
          else
               {
                char  * message ="LOFF"; 
                sendtocloud(message,4);
               }
  delay(1000);
          }
  else if (component=="TV")
            {
             
          if (digitalRead(pinoutB)==HIGH)
               {
                 char * message ="TVON"; 
                 sendtocloud(message,3);
                 } 
          else
               {
                char  * message ="TVOFF"; 
                sendtocloud(message,4);
               }
  delay(1000);
            }
      
  
  
  }