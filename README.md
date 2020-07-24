# goesrecv monitor

[![GitHub release](https://img.shields.io/github/release/sam210723/goesrecv-monitor.svg)](https://github.com/sam210723/goesrecv-monitor/releases/latest)
[![Github all releases](https://img.shields.io/github/downloads/sam210723/goesrecv-monitor/total.svg)](https://github.com/sam210723/goesrecv-monitor/releases/latest)
[![GitHub license](https://img.shields.io/github/license/sam210723/goesrecv-monitor.svg)](https://github.com/sam210723/goesrecv-monitor/master/LICENSE)

goesrecv monitor is a software utility for monitoring the status of [goesrecv](https://github.com/pietern/goestools) by [Pieter Noordhuis](https://twitter.com/pnoordhuis). goesrecv is a BPSK demodulator and CCSDS decoder for LRIT and HRIT downlinks transmitted by geostationary weather satellites like GOES-16/17 and GK-2A.

<p align="center"><img src="main-window.png"></p>

## Getting Started
Microsoft [.NET Framework Runtime v4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48) ([direct download](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-web-installer)) is required to run **goesrecv monitor**. Once .NET is installed, download the [latest release](https://github.com/sam210723/goesrecv-monitor/releases/latest/download/goesrecv-monitor.zip) of **goesrecv monitor** and extract all files inside the ZIP to a new folder.

On the device running goesrecv, open ```goesrecv.conf``` and confirm the following lines are not commented (remove ```#```), then restart goesrecv.
```
[clock_recovery.sample_publisher]
bind = "tcp://0.0.0.0:5002"
send_buffer = 2097152

[demodulator.stats_publisher]
bind = "tcp://0.0.0.0:6001"

[decoder.stats_publisher]
bind = "tcp://0.0.0.0:6002"
```

Finally, open **goesrecv monitor** and enter the IP address of a device running goesrecv, then click ```Connect``` (or hit enter). The constellation plot and statistics list will start showing data.

If **goesrecv monitor** fails to connect, check for firewalls on the device running goesrecv. Inbound connections on ports ```5002```, ```6001``` and ```6002``` must be allowed.

## Large Statistics
The large statistics window is intended to improve visibility of the Viterbi error count and Signal Quality percentage from a distance. This is useful while while fine tuning the alignment of an antenna. Launch the Large Statistics window by clicking on the "Large" button in the main window.

<p align="center"><img src="large-stats.png"></p>

## Debug Logs
**goesrecv monitor** can log certain information to a text file for the purposes of debugging crashes or configuration issues. Changing the ```logging``` setting in ```goesrecv-monitor.exe.config``` to either ```True``` or ```False``` will enable or disable the log file.
```
<setting name="logging" serializeAs="String">
    <value>False</value>
</setting>
```

```
<setting name="logging" serializeAs="String">
    <value>True</value>
</setting>
```
