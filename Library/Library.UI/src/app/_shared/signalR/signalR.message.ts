import { Severities } from "../messageNotifier/messageNotifier.service";

export interface ISignalRMessage {
    type: Severities;
    payload: string;
}

export class SignalRMessage implements ISignalRMessage {
    
    type: Severities;
    payload: string;
    
    constructor(type: Severities, payload: string) {
        this.type = type;
        this.payload = payload;
    }

    
}