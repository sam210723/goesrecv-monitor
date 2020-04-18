# goesrecv monitor
**goesrecv monitor** is a software utility for monitoring the status of [goesrecv](https://github.com/pietern/goestools) by [Pieter Noordhuis](https://twitter.com/pnoordhuis). goesrecv is a BPSK demodulator and CCSDS decoder for LRIT and HRIT downlinks transmitted by geostationary weather satellites like GOES-16/17 and GK-2A.

<p align="center"><img src="screenshot.png"></p>

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
