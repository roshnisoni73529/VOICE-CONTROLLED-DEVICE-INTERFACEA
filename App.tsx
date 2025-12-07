import React, { useState, useEffect, useRef } from 'react';
import { Mic, MicOff, Lightbulb, Fan, Thermometer, Volume2, Home, Wifi } from 'lucide-react';

interface Device {
  id: string;
  name: string;
  type: 'light' | 'fan' | 'ac' | 'speaker';
  isOn: boolean;
  value?: number;
  icon: React.ComponentType<any>;
}

const VoiceControlInterface: React.FC = () => {
  const [isListening, setIsListening] = useState(false);
  const [transcript, setTranscript] = useState('');
  const [confidence, setConfidence] = useState(0);
  const [devices, setDevices] = useState<Device[]>([
    { id: 'light1', name: 'Living Room Light', type: 'light', isOn: false, icon: Lightbulb },
    { id: 'light2', name: 'Bedroom Light', type: 'light', isOn: true, icon: Lightbulb },
    { id: 'fan1', name: 'Ceiling Fan', type: 'fan', isOn: false, value: 0, icon: Fan },
    { id: 'ac1', name: 'Air Conditioner', type: 'ac', isOn: false, value: 22, icon: Thermometer },
    { id: 'speaker1', name: 'Smart Speaker', type: 'speaker', isOn: true, value: 50, icon: Volume2 },
  ]);

  const recognitionRef = useRef<SpeechRecognition | null>(null);

  useEffect(() => {
    if ('webkitSpeechRecognition' in window || 'SpeechRecognition' in window) {
      const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
      recognitionRef.current = new SpeechRecognition();
      recognitionRef.current.continuous = true;
      recognitionRef.current.interimResults = true;

      recognitionRef.current.onresult = (event) => {
        const current = event.resultIndex;
        const transcript = event.results[current][0].transcript;
        const confidence = event.results[current][0].confidence;
        
        setTranscript(transcript);
        setConfidence(confidence);
        
        if (event.results[current].isFinal) {
          processVoiceCommand(transcript.toLowerCase());
        }
      };

      recognitionRef.current.onend = () => {
        setIsListening(false);
      };

      recognitionRef.current.onerror = (event) => {
        console.error('Speech recognition error:', event.error);
        setIsListening(false);
      };
    }

    return () => {
      if (recognitionRef.current) {
        recognitionRef.current.stop();
      }
    };
  }, []);

  const processVoiceCommand = (command: string) => {
    setDevices(prevDevices => {
      return prevDevices.map(device => {
        const deviceNameMatch = command.includes(device.name.toLowerCase()) || 
                               command.includes(device.type);
        
        if (deviceNameMatch) {
          if (command.includes('turn on') || command.includes('switch on')) {
            return { ...device, isOn: true };
          } else if (command.includes('turn off') || command.includes('switch off')) {
            return { ...device, isOn: false };
          } else if (command.includes('increase') || command.includes('up')) {
            return { 
              ...device, 
              value: Math.min((device.value || 0) + 10, 100),
              isOn: true 
            };
          } else if (command.includes('decrease') || command.includes('down')) {
            return { 
              ...device, 
              value: Math.max((device.value || 0) - 10, 0) 
            };
          }
        }
        return device;
      });
    });
  };

  const toggleListening = () => {
    if (isListening) {
      recognitionRef.current?.stop();
      setIsListening(false);
    } else {
      recognitionRef.current?.start();
      setIsListening(true);
      setTranscript('');
    }
  };

  const toggleDevice = (deviceId: string) => {
    setDevices(prevDevices =>
      prevDevices.map(device =>
        device.id === deviceId ? { ...device, isOn: !device.isOn } : device
      )
    );
  };

  const updateDeviceValue = (deviceId: string, value: number) => {
    setDevices(prevDevices =>
      prevDevices.map(device =>
        device.id === deviceId ? { ...device, value, isOn: value > 0 } : device
      )
    );
  };

  const getDeviceColor = (device: Device) => {
    if (!device.isOn) return 'text-gray-400';
    switch (device.type) {
      case 'light': return 'text-yellow-400';
      case 'fan': return 'text-blue-400';
      case 'ac': return 'text-cyan-400';
      case 'speaker': return 'text-green-400';
      default: return 'text-blue-400';
    }
  };

  const getDeviceBg = (device: Device) => {
    if (!device.isOn) return 'from-gray-800 to-gray-900';
    switch (device.type) {
      case 'light': return 'from-yellow-500/20 to-orange-500/20';
      case 'fan': return 'from-blue-500/20 to-cyan-500/20';
      case 'ac': return 'from-cyan-500/20 to-blue-500/20';
      case 'speaker': return 'from-green-500/20 to-emerald-500/20';
      default: return 'from-blue-500/20 to-purple-500/20';
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-900 via-gray-800 to-gray-900 text-white">
      {/* Header */}
      <div className="container mx-auto px-6 py-8">
        <div className="flex items-center justify-between mb-8">
          <div className="flex items-center space-x-4">
            <div className="p-3 bg-gradient-to-r from-blue-500 to-purple-600 rounded-2xl">
              <Home className="w-8 h-8" />
            </div>
            <div>
              <h1 className="text-3xl font-bold bg-gradient-to-r from-blue-400 to-purple-400 bg-clip-text text-transparent">
                Smart Home Control
              </h1>
              <p className="text-gray-400">Voice-controlled device management</p>
            </div>
          </div>
          <div className="flex items-center space-x-2 text-green-400">
            <Wifi className="w-5 h-5" />
            <span className="text-sm">Connected</span>
          </div>
        </div>

        {/* Voice Control Section */}
        <div className="mb-12">
          <div className="bg-gray-800/50 backdrop-blur-xl rounded-3xl p-8 border border-gray-700/50">
            <div className="text-center mb-6">
              <h2 className="text-2xl font-semibold mb-2">Voice Assistant</h2>
              <p className="text-gray-400">Say commands like "Turn on living room light" or "Increase fan speed"</p>
            </div>
            
            <div className="flex justify-center mb-6">
              <button
                onClick={toggleListening}
                className={`relative p-6 rounded-full transition-all duration-300 transform hover:scale-105 ${
                  isListening 
                    ? 'bg-gradient-to-r from-red-500 to-pink-500 shadow-lg shadow-red-500/30 animate-pulse' 
                    : 'bg-gradient-to-r from-blue-500 to-purple-600 shadow-lg shadow-blue-500/30 hover:shadow-blue-500/40'
                }`}
              >
                {isListening ? (
                  <MicOff className="w-8 h-8" />
                ) : (
                  <Mic className="w-8 h-8" />
                )}
                {isListening && (
                  <div className="absolute inset-0 rounded-full border-4 border-red-400 animate-ping"></div>
                )}
              </button>
            </div>

            {isListening && (
              <div className="bg-gray-900/50 rounded-2xl p-6 border border-gray-600/30">
                <div className="flex items-center justify-between mb-4">
                  <span className="text-sm font-medium text-gray-300">Listening...</span>
                  <span className="text-sm text-gray-400">
                    Confidence: {Math.round(confidence * 100)}%
                  </span>
                </div>
                <div className="bg-gray-800 rounded-xl p-4 min-h-[60px] flex items-center">
                  <p className="text-lg text-blue-300">
                    {transcript || "Speak now..."}
                  </p>
                </div>
              </div>
            )}
          </div>
        </div>

        {/* Devices Grid */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {devices.map((device) => {
            const IconComponent = device.icon;
            return (
              <div
                key={device.id}
                className={`bg-gradient-to-br ${getDeviceBg(device)} backdrop-blur-xl rounded-3xl p-6 border border-gray-700/50 transition-all duration-300 hover:scale-105 hover:shadow-2xl`}
              >
                <div className="flex items-center justify-between mb-4">
                  <div className="flex items-center space-x-3">
                    <div className={`p-3 rounded-2xl ${device.isOn ? 'bg-white/10' : 'bg-gray-700/50'}`}>
                      <IconComponent className={`w-6 h-6 ${getDeviceColor(device)} transition-colors duration-300`} />
                    </div>
                    <div>
                      <h3 className="font-semibold text-lg">{device.name}</h3>
                      <p className="text-sm text-gray-400 capitalize">{device.type}</p>
                    </div>
                  </div>
                  
                  <button
                    onClick={() => toggleDevice(device.id)}
                    className={`relative w-14 h-8 rounded-full transition-colors duration-300 ${
                      device.isOn ? 'bg-gradient-to-r from-green-400 to-emerald-500' : 'bg-gray-600'
                    }`}
                  >
                    <div
                      className={`absolute top-1 w-6 h-6 bg-white rounded-full shadow-lg transition-transform duration-300 ${
                        device.isOn ? 'translate-x-7' : 'translate-x-1'
                      }`}
                    />
                  </button>
                </div>

                <div className="flex items-center justify-between">
                  <span className={`text-sm font-medium ${device.isOn ? 'text-green-400' : 'text-gray-500'}`}>
                    {device.isOn ? 'ON' : 'OFF'}
                  </span>
                  {device.value !== undefined && (
                    <div className="flex items-center space-x-3">
                      <span className="text-sm text-gray-400 min-w-[3rem]">
                        {device.type === 'ac' ? `${device.value}°C` : `${device.value}%`}
                      </span>
                      <input
                        type="range"
                        min="0"
                        max={device.type === 'ac' ? '30' : '100'}
                        value={device.value}
                        onChange={(e) => updateDeviceValue(device.id, parseInt(e.target.value))}
                        className="w-20 h-2 bg-gray-700 rounded-lg appearance-none cursor-pointer slider"
                      />
                    </div>
                  )}
                </div>

                {device.isOn && (
                  <div className="mt-4 h-1 bg-gray-700 rounded-full overflow-hidden">
                    <div 
                      className={`h-full bg-gradient-to-r ${
                        device.type === 'light' ? 'from-yellow-400 to-orange-500' :
                        device.type === 'fan' ? 'from-blue-400 to-cyan-500' :
                        device.type === 'ac' ? 'from-cyan-400 to-blue-500' :
                        'from-green-400 to-emerald-500'
                      } transition-all duration-500`}
                      style={{ width: `${device.value || 100}%` }}
                    />
                  </div>
                )}
              </div>
            );
          })}
        </div>

        {/* Status Footer */}
        <div className="mt-12 text-center">
          <div className="inline-flex items-center space-x-4 bg-gray-800/30 backdrop-blur-xl rounded-2xl px-6 py-4 border border-gray-700/50">
            <div className="flex items-center space-x-2">
              <div className="w-3 h-3 bg-green-400 rounded-full animate-pulse"></div>
              <span className="text-sm text-gray-300">System Online</span>
            </div>
            <div className="w-px h-6 bg-gray-600"></div>
            <span className="text-sm text-gray-400">
              {devices.filter(d => d.isOn).length} of {devices.length} devices active
            </span>
          </div>
        </div>
      </div>

      <style jsx>{`
        .slider::-webkit-slider-thumb {
          appearance: none;
          width: 16px;
          height: 16px;
          border-radius: 50%;
          background: linear-gradient(45deg, #3B82F6, #8B5CF6);
          cursor: pointer;
          box-shadow: 0 2px 8px rgba(59, 130, 246, 0.4);
        }
        
        .slider::-moz-range-thumb {
          width: 16px;
          height: 16px;
          border-radius: 50%;
          background: linear-gradient(45deg, #3B82F6, #8B5CF6);
          cursor: pointer;
          border: none;
          box-shadow: 0 2px 8px rgba(59, 130, 246, 0.4);
        }
      `}</style>
    </div>
  );
};

export default VoiceControlInterface;